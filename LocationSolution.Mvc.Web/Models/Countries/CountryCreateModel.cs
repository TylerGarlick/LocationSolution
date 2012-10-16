namespace LocationSolution.Mvc.Web.Models.Countries
{
    public class CountryCreateModel : BasicCountryModel
    {
        public CountryCreateModel()
        {
            Displayable = true;
        }

        public CountryCreateModel(int countryOrder = 0)
        {
            CountryOrder = countryOrder;
            Displayable = true;
        }
    }
}