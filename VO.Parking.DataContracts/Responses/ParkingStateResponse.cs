using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VO.Parking.DataContracts.Responses
{
    public class ParkingStateResponse
    {
        public int AvailableParkingLots { get; set; }
        public int TotalParkingLots { get; set; }
        public int TotalCarsParkedCount { get; set; }
        public DateTime LastCarEntryDate { get; set; }
        public DateTime LastCarLeaveDate { get; set; }
    }
}
