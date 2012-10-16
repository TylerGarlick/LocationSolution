using System.ComponentModel.DataAnnotations;

namespace LocationSolution.Mvc.Web.Models.Cities
{
    public class BasicCityModel
    {
        [Required, Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Required, Display(Name = "State")]
        public int StateId { get; set; }

        [Required, Display(Name = "City Name"), StringLength(100, MinimumLength = 3)]
        public string CityName { get; set; }

        [Display(Name = "Nick Name"), StringLength(30)]
        public string NickName { get; set; }
    }
}