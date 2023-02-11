using System;
using System.Configuration;

namespace VO.Parking
{
    public class Settings : ISettings
    {
        public string ParkingDbConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ParkingDbConnectionString"].ConnectionString; }
        }

        public string ImageUploadPholder
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageUploadPholder"].ToString();
            }
        }
    }
}