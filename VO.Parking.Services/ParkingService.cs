using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VO.Parking.Entities;
using VO.Parking.Services.Caching;

namespace VO.Parking.Services
{
    public class ParkingService : IParkingService
    {

        private ParkingDbContext context;

        private ICache cacheManager;

        private ISettings settings;

        public ParkingService(ISettings settings, ICache cacheManager)
        {
            this.context = new ParkingDbContext();
            this.cacheManager = cacheManager;
            this.settings = settings;
        }

        public List<Car> GetAllCars()
        {
            return this.context.Cars.ToList();

            //if (!cacheManager.IsSet(CacheKeys.AllCars))
            //{
            //var cars = this.context.Cars.ToList();
            //cacheManager.Set(CacheKeys.AllCars, cars);
            //}
            //return cacheManager.Get<List<Car>>(CacheKeys.AllCars);
        }

        public ParkingState GetParkingState()
        {
            return this.context.ParkingStates.FirstOrDefault();
        }

        public List<Car> GetParkingCars()
        {
            return this.context.ParkingCars.Include(c => c.Car).Select(c => c.Car).ToList();
        }

        public Car GetCar(int id)
        {
            return this.context.Cars.Find(id);
        }

        public Car GetCar(string licenseNumber)
        {
            return this.context.Cars.Where(c => c.LicenseNumber == licenseNumber).FirstOrDefault();
        }

        public Car AddCar(string licenseNumber)
        {
            Car car = GetCar(licenseNumber);
            if(car != null)
            {
                car.ParkingUsedCount ++;
                car.LastTimeEntryDate = DateTime.Now;
                car.CarParkingStatus = 1;
                this.context.Entry(car).State = EntityState.Modified;
                this.context.SaveChanges();
                
                return car;
            }

            car = new Car()
            {
                LicenseNumber = licenseNumber,
                LastTimeEntryDate = DateTime.Now,
                FirtsTimeEntryDate= DateTime.Now,
                ParkingUsedCount = 1,
                CarParkingStatus = 1
            };

            this.context.Cars.Add(car);
            this.context.SaveChanges();

            cacheManager.Remove(CacheKeys.AllCars);
            return car;
        }

        public Car CarIsLeaving(string licenseNumber)
        {
            Car car = GetCar(licenseNumber);
            if(car == null)
            {
                return null;
            }

            car.CarParkingStatus = 0;
            this.context.Entry(car).State = EntityState.Modified;
            this.context.SaveChanges();

            return car;
        }

        public bool ParkingIsAvailbale()
        {
            ParkingState state =  GetParkingState();
            
            return state.AvailableParkingLots > 0;
        }
    }
}
