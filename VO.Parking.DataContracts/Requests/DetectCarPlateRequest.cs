using ServiceStack;

using System;
using System.IO;

using VO.Parking.DataContracts.Responses;

namespace VO.Parking.DataContracts.Requests
{
    [Route("/detect")]
    public class DetectCarPlateRequest : IReturn<DetectedCarPlateResponse>
    {
        //[Input(Type = "file"), UploadTo("profiles")]
        public string ImageFileUrl { get; set; }
    }
}
