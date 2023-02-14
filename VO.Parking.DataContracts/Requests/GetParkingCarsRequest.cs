using ServiceStack;
using VO.Parking.DataContracts.Responses;


namespace VO.Parking.DataContracts.Requests
{
    [Route("/parking-cars")]
    public class GetParkingCarsRequest : IReturn<CarsResponse>
    { 
    }
}
