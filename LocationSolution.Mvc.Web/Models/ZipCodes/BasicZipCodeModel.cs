using System.ComponentModel.DataAnnotations;

namespace LocationSolution.Mvc.Web.Models.ZipCodes
{
    public class BasicZipCodeModel
    {
        [Required, Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Required, Display(Name = "State")]
        public int StateId { get; set; }

        [Required, Display(Name = "City")]
        public int CityId { get; set; }

        [Required, StringLength(15, MinimumLength = 5, ErrorMessage = "Zip must be between 5 and 15 characters")]
        public string Zip { get; set; }

        [Range(double.MinValue, double.MaxValue)]
        public double? Latitude { get; set; }

        [Range(double.MinValue, double.MaxValue)]
        public double? Longitude { get; set; }
    }
}