using System.Collections.Generic;
using LocationSolution.Data.Core;

namespace LocationSolution.Services.Common
{
    public interface ICitiesService : ISharedOperations<City>
    {
        IEnumerable<City> SearchByCityNameOrNickName(string query);
        IEnumerable<City> GetAllCitiesByStateIdAndCountryCode(string countryCode, int stateId);
        City GetCityByCityId(int id);
        City GetCityByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId);
    }
}