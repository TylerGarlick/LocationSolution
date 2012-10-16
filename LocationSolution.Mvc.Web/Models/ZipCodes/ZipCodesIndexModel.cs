using LocationSolution.Data.Core;
using System.Collections.Generic;

namespace LocationSolution.Mvc.Web.Models.ZipCodes
{
    public class ZipCodesIndexModel
    {
        public IEnumerable<ZipCode> ZipCodes { get; set; }
    }
}