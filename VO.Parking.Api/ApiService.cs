using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ServiceStack;
using ServiceStack.Logging;
//using VO.Parking.DataContracts.Requests;
//using VO_Biblioteca.LibrarieModele;
using VO.Parking.Services;

using VO.Parking.DataContracts.Requests;
using VO.Parking.DataContracts.Responses;
using System.Drawing;
using System.IO;
using ServiceStack.Web;
using System.Security.Cryptography;
using System.Text;

namespace VO.Parking.API
{
    public class ApiService : Service
    {
        private readonly ILog log;

        private readonly IParkingService parkingService;

        private readonly IMapper mapper;

        private readonly ISettings settings;

        public ApiService(ILog log, IParkingService parkingService, IMapper mapper, ISettings settings)
        {
            this.log = log;
            this.parkingService = parkingService;
            this.mapper = mapper;
            this.settings = settings;
        }

        public object Any(AllCarsRequest carsRequest)
        {
            this.log.Info("CarsApi.GetAllCarsRequest");
            try
            {
                List<Entities.Car> cars = this.parkingService.GetAllCars();
                return this.mapper.Map<List<Entities.Car>, List<DataContracts.Car>>(cars);
            }
            catch(Exception ex)
            {
                this.log.Error($"CarsApi.GetAllCarsRequest \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        public object Any(AddCarRequest addCarRequest)
        {
            this.log.Info($"CarsApi.AddCarRequest, carLicenceNumber: {addCarRequest.LicenseNumber}");

            try
            {
                var car = this.parkingService.AddCar(addCarRequest.LicenseNumber);

                return this.mapper.Map<Entities.Car, DataContracts.Car>(car);
            }
            catch (Exception ex)
            {
                this.log.Error($"CarsApi.AddCarRequest, carLicenceNumber: {addCarRequest.LicenseNumber} \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        public object Any(LeaveRequest leaveRequest)
        {
            this.log.Info($"CarsApi.LeaveRequest, carLicenceNumber: {leaveRequest.LicenseNumber}");

            try
            {
                var car = this.parkingService.CarIsLeaving(leaveRequest.LicenseNumber);

                return
                    new LeaveResponse
                    {
                        Car = this.mapper.Map<Entities.Car, DataContracts.Car>(car),
                        StayedTime = DateTime.Now.Subtract(car.LastTimeEntryDate)
                    };
            }
            catch (Exception ex)
            {
                this.log.Error($"CarsApi.LeaveRequest, carLicenceNumber: {leaveRequest.LicenseNumber} \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        public object Post(DetectCarPlateRequest detectCarPlateRequest)
        {
            this.log.Info($"CarsApi.DetectCarPlateRequest, imageUrl: {detectCarPlateRequest.ImageFileUrl}");

            try
            {
                string filePath = SaveImage(detectCarPlateRequest.ImageFileUrl);

                // TODO: call a python service to detect Plate License Number


                // TODO: return recognized Plate License Number
                return new DetectedCarPlateResponse { LicenseNumber = string.Empty };
            }
            catch (Exception ex)
            {
                this.log.Error($"CarsApi.DetectCarPlateRequest, imageUrl: {detectCarPlateRequest.ImageFileUrl} \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        private string SaveImage(string imageUrl)
        {
            if (imageUrl != null)
            {
                using (var ms = new MemoryStream(imageUrl.GetBytesFromUrl()))
                {
                    return WriteImage(ms);
                }
            }
            return null;
        }

        private string WriteImage(Stream ms)
        {
            var hash = GetMd5Hash(ms);

            ms.Position = 0;
            var fileName = hash + ".png";
            using (var img = Image.FromStream(ms))
            {
                string uploadsDir = this.settings.ImageUploadPholder.MapHostAbsolutePath();
                string filePath = uploadsDir.CombineWith(fileName);
                img.Save(filePath);

                return filePath;
            }
        }

        public static string GetMd5Hash(Stream stream)
        {
            var hash = MD5.Create().ComputeHash(stream);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}