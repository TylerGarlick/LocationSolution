using LocationSolution.Data.Core;
using System.Collections.Generic;
using PagedList;

namespace LocationSolution.Mvc.Web.Models.States
{
    public class StatesIndexModel
    {
        public IEnumerable<State> States { get; set; }
        public IPagedList<State> StateResults { get; set; }
    }
}