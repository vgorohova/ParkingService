using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VO.Parking.DataContracts
{
    [Serializable]
    public class Car
    {
        public int Id { get; set; }

        public string LicenseNumber { get; set; }

        public int ParkingUsedCount { get; set; }

        public DateTime FirtsTimeEntryDate { get; set; }

        public DateTime LastTimeEntryDate { get; set; }

        public int CarParkingStatus { get; set; }
    }
}
