using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.Cities
{
    public class CityUpdateModel : BasicCityModel
    {
        public CityUpdateModel() { }

        public CityUpdateModel(City city)
        {
            CountryCode = city.CountyCode;
            StateId = city.StateId;
            CityName = city.CityName;
            NickName = city.NickName;
        }
    }

    public class CityDeleteModel
    {
        public City City { get; set; }

        public CityDeleteModel()
        {

        }

        public CityDeleteModel(City city)
        {
            City = city;
        }
    }
}