using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace VO.Parking.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // New code
            var cors = new EnableCorsAttribute("parkingweb.localdev", "*", "*");
            config.EnableCors(cors);
        }
    }
}