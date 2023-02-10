using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

using Funq;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;

using VO.Parking.Services;
using VO.Parking.Services.Caching;

namespace VO.Parking.API
{
    public class AppHost : AppHostBase
    {
        // Tell Service Stack the name of your application and where to find your web services
        public AppHost()
            : base("VO.Parking.API", typeof(ApiService).Assembly)
        {
            LogManager.LogFactory = new NLogFactory();
        }

        public override void Configure(Container container)
        {
            // register any dependencies your services use, e.g:
            container.Register<ILog>(LogManager.GetLogger(typeof(NLog.Logger)));

            var settings = new Settings();
            container.Register<ISettings>(settings);

            container.Register<IMapper>(AutoMapperConfiguration.CreateMapper(container.Resolve<ISettings>()));

            container.Register<ICache>(new MemoryCacheService());

            //container.Register<IBibliotecaRepository>(
            //    new BibliotecaDBContext(settings.DbBibliotecaConnection));

            container.Register<IParkingService>(
                new ParkingService(container.Resolve<ISettings>(), container.Resolve<ICache>()));

            SetConfig(new HostConfig
            {
                GlobalResponseHeaders = {
                  { "Access-Control-Allow-Origin", "*" },
                  { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                  { "Access-Control-Allow-Headers", "Content-Type" },
                },
            });
        }
    }
}