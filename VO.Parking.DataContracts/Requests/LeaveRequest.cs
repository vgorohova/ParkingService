using ServiceStack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VO.Parking.DataContracts.Responses;

namespace VO.Parking.DataContracts.Requests
{
    [Route("/leave")]
    public class LeaveRequest : IReturn<LeaveResponse>
    {
        public string LicenseNumber { get; set; }
    }
}
