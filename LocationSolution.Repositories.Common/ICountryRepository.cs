using System.Linq;
using LocationSolution.Data.Core;
using NexBusiness.Repositories.Common;

namespace LocationSolution.Repositories.Common
{
    public interface ICountryRepository : IRepository<Country>
    {
        IQueryable<Country> SearchByCountryCodeOrName(string query);
        IQueryable<Country> AllActive();
    }
}
