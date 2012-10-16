using LocationSolution.Data.Core;
using System.Collections.Generic;
using PagedList;

namespace LocationSolution.Mvc.Web.Models.ZipCodes
{
    public class ZipCodesIndexModel
    {
        public IEnumerable<ZipCode> ZipCodes { get; set; }
        public IPagedList<ZipCode> ZipCodeResults { get; set; }
    }
}