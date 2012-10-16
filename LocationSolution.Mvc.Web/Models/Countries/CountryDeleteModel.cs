using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.Countries
{
    public class CountryDeleteModel
    {
        public CountryDeleteModel()
        {
        }

        public CountryDeleteModel(Country country)
        {
            Country = country;
        }

        public Country Country { get; set; }
    }
}