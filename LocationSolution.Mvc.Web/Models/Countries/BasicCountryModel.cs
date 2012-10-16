using System.ComponentModel.DataAnnotations;

namespace LocationSolution.Mvc.Web.Models.Countries
{
    public class BasicCountryModel
    {
        public BasicCountryModel(int countryOrder = 0)
        {
            CountryOrder = countryOrder;
        }

        [Required, Display(Name = "Country Code"), StringLength(3, MinimumLength = 3, ErrorMessage = "Country Code must be 3 characters.")]
        public string CountryCode { get; set; }

        [Required, Display(Name = "Country Name"), StringLength(100, MinimumLength = 3, ErrorMessage = "Country Name must be at least 3 characters and no more than 100 characters.")]
        public string CountryName { get; set; }

        [Display(Name = "Country Order"), Range(0, int.MaxValue)]
        public int? CountryOrder { get; set; }

        public bool Displayable { get; set; }
    }
}