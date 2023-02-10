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
                this.log.Error($"CarsApi.GetAllCarsRequest exception: {ex.Message}");
                return null;
            }
        }

        public object Any(AddCarRequest addCarRequest)
        {
            this.log.Info($"CarsApi.AddCarRequest, carLicenceNumber: {addCarRequest.LicenceNumber}");

            try
            {
                var car = this.parkingService.AddCar(addCarRequest.LicenceNumber);

                return this.mapper.Map<Entities.Car, DataContracts.Car>(car);
            }
            catch (Exception ex)
            {
                this.log.Error($"CarsApi.AddCarRequest, carLicenceNumber: {addCarRequest.LicenceNumber} \n exception: {ex.Message} \n {ex.InnerException}");
                return null;
            }
        }

        //public object Any(GetCarRequest)



        //public object Any(GenresRequest genresRequest)
        //{
        //    this.log.Info("ApiService.GenresRequest");

        //    try
        //    {
        //        var genres = this.bibliotecaService.GetAllGenres();

        //        return this.mapper.Map<List<Genre>, List<DataContracts.Genre>>(genres);
        //    }
        //    catch(Exception ex) 
        //    {
        //        this.log.Error($"ApiService.GenresRequest exception: {ex.Message}");
        //        return null;
        //    }
        //}

        //public object Any(GenreRequest genreRequest)
        //{
        //    this.log.Info($"ApiService.GenreRequest, id: {genreRequest.id}");

        //    try
        //    {
        //        var genre = this.bibliotecaService.GetGenre(genreRequest.id);

        //        return this.mapper.Map<Genre, DataContracts.Genre>(genre);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.log.Error($"ApiService.GenreRequest, id: {genreRequest.id} exception: {ex.Message}");
        //        return null;
        //    }
        //}

        //public object Any(CreateGenreRequest createGenreRequest)
        //{
        //    this.log.Info($"ApiService.CreateGenreRequest, genreName: {createGenreRequest.GenreName}");

        //    try
        //    {
        //        var genre = this.bibliotecaService.AddGenre(createGenreRequest.GenreName);

        //        return this.mapper.Map<Genre, DataContracts.Genre>(genre);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.log.Error($"ApiService.CreateGenreRequest, genreName: {createGenreRequest.GenreName} exception: {ex.Message}");
        //        return null;
        //    }
        //}
    }
}