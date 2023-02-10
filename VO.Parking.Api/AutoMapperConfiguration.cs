using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VO.Parking.API
{
    using AutoMapper;
    //using VO_Biblioteca.LibrarieModele;

    public class AutoMapperConfiguration
    {
        public static IMapper CreateMapper(ISettings settings)
        {
            var config = new MapperConfiguration(
                cfg =>
                    {
                        cfg.CreateMap<Entities.Car, DataContracts.Car>();
                    }
                );

            return config.CreateMapper();

        }
    }
}