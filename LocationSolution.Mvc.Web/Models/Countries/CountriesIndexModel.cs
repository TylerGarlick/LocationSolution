using System.Collections.Generic;
using LocationSolution.Data.Core;
using PagedList;

namespace LocationSolution.Mvc.Web.Models.Countries
{
    public class CountriesIndexModel
    {
        public IEnumerable<Country> Countries { get; set; }
        public IPagedList<Country> CountriesResults { get; set; }
    }
}