using LocationSolution.Data.Core;
using System.Collections.Generic;

namespace LocationSolution.Mvc.Web.Models.States
{
    public class StatesIndexModel
    {
        public IEnumerable<State> States { get; set; }
    }
}