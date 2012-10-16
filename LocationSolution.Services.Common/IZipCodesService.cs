using System.Collections.Generic;
using LocationSolution.Data.Core;

namespace LocationSolution.Services.Common
{
    public interface IZipCodesService : ISharedOperations<ZipCode>
    {
        IEnumerable<ZipCode> SearchByZipcode(string query);
        IEnumerable<ZipCode> GetAllZipsByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId);
        ZipCode GetByZipCode(string zipCode);
        ZipCode GetByCountryCodeAndStateIdAndCityIdAndZipCode(string countryCode, int stateId, int cityId, string zipCode);
    }
}