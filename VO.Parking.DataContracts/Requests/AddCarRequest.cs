using ServiceStack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VO.Parking.DataContracts.Responses;

namespace VO.Parking.DataContracts.Requests
{
    [Route("/add-car")]
    public class AddCarRequest : IReturn<CarResponse>
    {
        public string LicenceNumber { get; set; }
    }
}
