using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VO.Parking.API
{
    using AutoMapper;

    public class AutoMapperConfiguration
    {
        public static IMapper CreateMapper(ISettings settings)
        {
            var config = new MapperConfiguration(
                cfg =>
                    {
                        cfg.CreateMap<Entities.Car, DataContracts.Car>();
                        cfg.CreateMap<Entities.ParkingState, DataContracts.Responses.ParkingStateResponse>();
                        cfg.CreateMap<Services.ParkingStatistics, DataContracts.Responses.ParkingStatsRecord>();
                    }
                );

            return config.CreateMapper();

        }
    }
}