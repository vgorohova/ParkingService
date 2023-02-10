using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VO.Parking
{
    public interface ISettings
    {
        string ParkingDbConnectionString { get; }
    }
}