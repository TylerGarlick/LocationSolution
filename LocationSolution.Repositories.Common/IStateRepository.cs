using System.Linq;
using LocationSolution.Data.Core;
using NexBusiness.Repositories.Common;

namespace LocationSolution.Repositories.Common
{
    public interface IStateRepository : IRepository<State>
    {
        IQueryable<State> ByCountryCode(string countryCode);
        IQueryable<State> SearchByStateNameOrStateCode(string countryCode, string query);
    }
}