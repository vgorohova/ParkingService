namespace VO.Parking.DataContracts.Responses
{
    public class DetectedCarPlateResponse
    {
        public string LicenseNumber { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
