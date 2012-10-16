using LocationSolution.Data.Core;
using System.Collections.Generic;

namespace LocationSolution.Mvc.Web.Models.Cities
{
    public class CitiesIndexModel
    {
        public IEnumerable<City> Cities { get; set; }
    }
}