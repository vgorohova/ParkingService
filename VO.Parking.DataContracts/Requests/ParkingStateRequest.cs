using ServiceStack;
using VO.Parking.DataContracts.Responses;

namespace VO.Parking.DataContracts.Requests
{
    [Route("/state")]
    public class ParkingStateRequest : IReturn<ParkingStateResponse>
    {
    }
}
