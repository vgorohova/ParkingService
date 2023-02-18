using System;
using System.Collections.Generic;

namespace VO.Parking.DataContracts.Responses
{
    public class ParkingStatsRecord
    {
        public int CarsCountPerDay { get; set; }
        public DateTime DayDate { get; set; }
    }
    public class ParkingStatsResponse
    {
        public List<ParkingStatsRecord> ParkingStatistics { get; set;}
        
    }
}
