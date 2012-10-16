using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.Countries
{
    public class CountryUpdateModel : BasicCountryModel
    {
        public CountryUpdateModel()
        {
        }

        public CountryUpdateModel(Country entity)
        {
            CountryCode = entity.CountryCode;
            CountryName = entity.CountryName;
            CountryOrder = entity.CountryOrder;
            Displayable = entity.Displayable.Equals("1");
        }
    }
}