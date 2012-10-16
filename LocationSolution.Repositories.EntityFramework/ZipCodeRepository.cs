using System;
using System.Data.Entity;
using System.Linq;
using LocationSolution.Data.Core;
using NexBusiness.Repositories.EntityFramework;
using LocationSolution.Repositories.Common;

namespace LocationSolution.Repositories.EntityFramework
{
    public class ZipCodeRepository : Repository<ZipCode, LocationsEntities>, IZipCodeRepository
    {
        public ZipCodeRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public override IQueryable<ZipCode> All()
        {
            return DbContext.ZipCodes.OrderBy(z => z.Zip);
        }

        public IQueryable<ZipCode> ByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId)
        {
            return All().Where(z => z.CountyCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase) && z.StateId == stateId && z.CityId == cityId);
        }

        public ZipCode ByCountryCodeAndStateIdAndCityIdAndZipCode(string countryCode, int stateId, int cityId, string zipCode)
        {
            return ByCountryCodeAndStateIdAndCityId(countryCode, stateId, cityId).FirstOrDefault(z => z.Zip.Equals(zipCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
