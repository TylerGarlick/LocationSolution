using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.ZipCodes
{
    public class ZipCodeUpdateModel : BasicZipCodeModel
    {

        public ZipCodeUpdateModel()
        {

        }

        public ZipCodeUpdateModel(ZipCode zipCode)
        {
            CountryCode = zipCode.CountyCode;
            StateId = zipCode.StateId;
            CityId = zipCode.CityId;
            Zip = zipCode.Zip;
            Longitude = zipCode.Longitude;
            Latitude = zipCode.Latitude;
        }
    }
}