using System.Linq;
using LocationSolution.Data.Core;
using NexBusiness.Repositories.Common;

namespace LocationSolution.Repositories.Common
{
    public interface IZipCodeRepository : IRepository<ZipCode>
    {
        IQueryable<ZipCode> ByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId);
        IQueryable<ZipCode> SearchByZipCode(string query);
        ZipCode ByCountryCodeAndStateIdAndCityIdAndZipCode(string countryCode, int stateId, int cityId, string zipCode);

    }
}