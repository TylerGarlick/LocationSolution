using System.Collections.Generic;
using LocationSolution.Data.Core;

namespace LocationSolution.Services.Common
{
    public interface IStatesService : ISharedOperations<State>
    {
        IEnumerable<State> SearchByStateNameOrCode(string query);
        IEnumerable<State> GetAllStatesByCountryCode(string countryCode);
        State GetStateByStateId(int id);
        State GetStateByCountryCodeAndStateId(string countryCode, int id);
    }
}