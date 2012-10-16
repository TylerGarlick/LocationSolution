using System.Collections.Generic;
using LocationSolution.Data.Core;

namespace LocationSolution.Services.Common
{
    public interface ICountriesService : ISharedOperations<Country>
    {
        IEnumerable<Country> GetAllActiveCountries();
        IEnumerable<Country> SearchByCountryNameOrCode(string query);
        Country GetCountryByCountryCode(string countryCode);
    }
}
