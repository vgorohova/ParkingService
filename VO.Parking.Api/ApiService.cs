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

namespace VO.Parking.API
{
    public class ApiService : Service
    {
        private readonly ILog log;

        private readonly IParkingService parkingService;

        private readonly IMapper mapper;

        public ApiService(ILog log, IParkingService parkingService, IMapper mapper)
        {
            this.log = log;
            this.parkingService = parkingService;
            this.mapper = mapper;
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

                return //this.mapper.Map<Entities.Car, DataContracts.Car>(car);
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
    }
}