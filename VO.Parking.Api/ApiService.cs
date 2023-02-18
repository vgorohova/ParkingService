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

        public object Any(ParkingStateRequest request)
        {
            this.log.Info("CarsApi.ParkingStateRequest");
            try
            {
                Entities.ParkingState state = this.parkingService.GetParkingState();
                return this.mapper.Map<Entities.ParkingState, ParkingStateResponse>(state);
            }
            catch (Exception ex)
            {
                this.log.Error($"CarsApi.ParkingStateRequest \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        public object Any(GetParkingCarsRequest request)
        {
            this.log.Info("CarsApi.GetParkingCarsRequest");
            try
            {
                var cars = this.parkingService.GetParkingCars();
                return this.mapper.Map<List<Entities.Car>, List<DataContracts.Car>>(cars);
            }
            catch (Exception ex)
            {
                this.log.Error($"CarsApi.GetParkingCarsRequest \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        public object Any(GetParkingPerDayStatsRequest request)
        {
            this.log.Info("CarsApi.GetParkingPerDayStatsRequest");
            try
            {
                var stats = this.parkingService.GetParkingStatisticsPerDay();
                return this.mapper.Map<List<ParkingStatistics>, List<ParkingStatsRecord>>(stats);
            }
            catch (Exception ex)
            {
                this.log.Error($"CarsApi.GetParkingPerDayStatsRequest \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        public object Any(AddCarRequest addCarRequest)
        {
            this.log.Info($"CarsApi.AddCarRequest, carLicenceNumber: {addCarRequest.LicenseNumber}");

            try
            {
                if (this.parkingService.ParkingIsAvailbale())
                {
                    var car = this.parkingService.AddCar(addCarRequest.LicenseNumber);

                    return
                        new CarResponse
                        {
                            Car = this.mapper.Map<Entities.Car, DataContracts.Car>(car),
                            IsSuccess = true
                        };
                }

                return
                        new CarResponse
                        {
                            IsSuccess = false,
                            ErrorMessage = "Parking is not available!"
                        };
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

                string requestUrl = this.settings.DetectLicenseNumberServiceUrl + HttpUtility.UrlEncode(filePath);

                var detectServiceResponse = requestUrl
                    .GetJsonFromUrl()
                    .FromJson<DetectedCarPlateResponse>();

                if (detectServiceResponse != null
                    && detectServiceResponse.IsSuccess)
                {
                    return new DetectedCarPlateResponse { 
                        LicenseNumber = detectServiceResponse.LicenseNumber, 
                        IsSuccess = true
                    };
                }

                return new DetectedCarPlateResponse { 
                    LicenseNumber = string.Empty, 
                    IsSuccess = false, 
                    ErrorMessage = "Failed to detect a license number." 
                };
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
                    return WriteImage(ms, imageUrl);
                }
            }
            return null;
        }

        private string WriteImage(Stream ms, string imageUrl)
        {
            //var hash = GetMd5Hash(ms);

            ms.Position = 0;
            //var fileName = hash + ".png";
            string fileName = Path.GetFileName(imageUrl);
            using (var img = Image.FromStream(ms))
            {
                string uploadsDir = this.settings.ImageUploadPholder.MapHostAbsolutePath();
                string filePath = uploadsDir.CombineWith(fileName);
                try
                {
                    img.Save(filePath);
                }
                catch(Exception ex) { }

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