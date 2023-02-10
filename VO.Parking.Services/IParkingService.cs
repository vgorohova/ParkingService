using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VO.Parking.Entities;

namespace VO.Parking.Services
{
    public interface IParkingService
    {
        List<Car> GetAllCars();

        Car GetCar(int id);

        Car GetCar(string licenseNumber);

        Car AddCar(string licenceNumber);
    }
}
