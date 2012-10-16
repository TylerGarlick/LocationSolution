using System;
using System.Data.Entity;
using System.Linq;
using LocationSolution.Data.Core;
using NexBusiness.Repositories.EntityFramework;
using LocationSolution.Repositories.Common;

namespace LocationSolution.Repositories.EntityFramework
{
    public class CityRepository : Repository<City, LocationsEntities>, ICityRepository
    {
        public CityRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public override IQueryable<City> All()
        {
            return DbContext.Cities.OrderBy(c => c.CityName);
        }

        public IQueryable<City> SearchByCityNameOrNickName(string query)
        {
            return All().Where(c => c.CityName.Contains(query) || c.NickName.Contains(query));
        }

        public IQueryable<City> ByCountryCodeAndStateId(string countryCode, int stateId)
        {
            return All().Where(c => c.CountyCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase) && c.StateId == stateId);
        }

        public City ByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId)
        {
            return ByCountryCodeAndStateId(countryCode, stateId).FirstOrDefault(c => c.CityId == cityId);
        }
    }
}
