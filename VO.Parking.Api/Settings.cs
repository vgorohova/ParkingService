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

        public string DetectLicenseNumberServiceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["DetectLicenseNumberServiceUrl"].ToString();
            }
        }

        public string ParkingDbConnectionStringSimple
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ParkingDbConnectionStringSimple"].ConnectionString;
            }
        }
    }
}