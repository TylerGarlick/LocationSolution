using System.Collections.Generic;
using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.Countries
{
    public class CountriesIndexModel
    {
        public IEnumerable<Country> Countries { get; set; }
    }
}