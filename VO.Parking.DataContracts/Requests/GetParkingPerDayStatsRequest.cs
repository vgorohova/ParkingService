using ServiceStack;

using VO.Parking.DataContracts.Responses;

namespace VO.Parking.DataContracts.Requests
{
    [Route("/parking-stats-day")]
    public class GetParkingPerDayStatsRequest : IReturn<ParkingStatsResponse>
    {
    }
}
