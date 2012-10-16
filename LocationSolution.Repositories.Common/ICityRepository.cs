using System.Linq;
using LocationSolution.Data.Core;
using NexBusiness.Repositories.Common;

namespace LocationSolution.Repositories.Common
{
    public interface ICityRepository : IRepository<City>
    {
        IQueryable<City> SearchByCityNameOrNickName(string query);
        IQueryable<City> ByCountryCodeAndStateId(string countryCode, int stateId);
        City ByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId);
    }
}