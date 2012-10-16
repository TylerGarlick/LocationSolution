using System.ComponentModel.DataAnnotations;

namespace LocationSolution.Mvc.Web.Models.States
{
    public class BasicStateModel
    {
        [Required, Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Required, StringLength(10, MinimumLength = 2, ErrorMessage = "State Code must be between 2 and 10 characters"), Display(Name = "State Code")]
        public string StateCode { get; set; }

        [Required, StringLength(30, MinimumLength = 2, ErrorMessage = "State Code must be between 2 and 30 characters"), Display(Name = "State Name")]
        public string StateName { get; set; }

        [Display(Name = "Contiguous Land")]
        public bool ContiguousLand { get; set; }

        [Display(Name = "Is Territory")]
        public bool IsTerritory { get; set; }
    }
}