using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.States
{
    public class StateUpdateModel : BasicStateModel
    {
        public StateUpdateModel() { }

        public StateUpdateModel(State state)
        {
            StateName = state.StateName;
            StateCode = state.StateCode;
            IsTerritory = state.IsTerritory == "1";
            ContiguousLand = state.ContiguousLand == "1";
            CountryCode = state.CountryCode;
        }
    }
}