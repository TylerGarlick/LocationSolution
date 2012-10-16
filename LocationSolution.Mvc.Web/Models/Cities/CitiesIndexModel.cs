using LocationSolution.Data.Core;
using System.Collections.Generic;
using PagedList;

namespace LocationSolution.Mvc.Web.Models.Cities
{
    public class CitiesIndexModel
    {
        public IEnumerable<City> Cities { get; set; }
        public IPagedList<City> CityResults { get; set; }
    }
}