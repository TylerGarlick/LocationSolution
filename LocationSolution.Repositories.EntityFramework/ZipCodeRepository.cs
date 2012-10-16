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

        public IQueryable<ZipCode> SearchByZipCode(string query)
        {
            var loweredQuery = query.ToLower();
            return All().Where(z => z.City.CityName.ToLower().Contains(loweredQuery) || z.City.NickName.ToLower().Contains(loweredQuery) || z.City.State.StateName.ToLower().Contains(loweredQuery) || z.City.State.StateCode.ToLower().Contains(loweredQuery) || z.City.State.Country.CountryName.ToLower().Contains(loweredQuery) || z.City.State.Country.CountryCode.ToLower().Contains(loweredQuery) || z.Zip.ToLower().Contains(loweredQuery));
        }

        public ZipCode ByCountryCodeAndStateIdAndCityIdAndZipCode(string countryCode, int stateId, int cityId, string zipCode)
        {
            return ByCountryCodeAndStateIdAndCityId(countryCode, stateId, cityId).FirstOrDefault(z => z.Zip.Equals(zipCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
