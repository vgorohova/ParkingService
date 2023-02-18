using System;
using System.Collections.Generic;
using VO.Parking.Entities;

namespace VO.Parking.Services
{
    public interface IParkingService
    {
        List<Car> GetAllCars();

        ParkingState GetParkingState();

        List<Car> GetParkingCars();

        Car GetCar(int id);

        Car GetCar(string licenseNumber);

        Car AddCar(string licenceNumber);

        Car CarIsLeaving(string licenseNumber);

        bool ParkingIsAvailbale();

        List<ParkingStatistics> GetParkingStatisticsPerDay();
    }
}
