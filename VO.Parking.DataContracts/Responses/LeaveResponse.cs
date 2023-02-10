using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VO.Parking.DataContracts.Responses
{
    public class LeaveResponse
    {
        public Car Car { get; set; }

        public TimeSpan StayedTime { get; set; }
    }
}
