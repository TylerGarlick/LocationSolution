using System.Data.Entity;
using LocationSolution.Data.Core;
using LocationSolution.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using NexBusiness.Repositories.EntityFramework;

namespace LocationSolution.Repositories.EntityFramework
{
    public class StateRepository : Repository<State, LocationsEntities>, IStateRepository
    {
        public StateRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public override IQueryable<State> All()
        {
            return DbContext.States.OrderBy(s => s.StateName);
        }

        public IQueryable<State> ByCountryCode(string countryCode)
        {
            var country = GetCountryByCountryCode(countryCode);
            if (country != null)
                return country.States.OrderBy(s => s.StateName).AsQueryable();

            throw new KeyNotFoundException(string.Format("Country Code {0} was not found", countryCode));
        }

        public IQueryable<State> SearchByStateNameOrStateCode(string countryCode, string query)
        {
            var country = GetCountryByCountryCode(countryCode);
            if (country != null)
                return country.States.OrderBy(s => s.StateName.Contains(query) || s.StateCode.Contains(query)).AsQueryable();

            throw new KeyNotFoundException(string.Format("Country Code {0} was not found", countryCode));
        }

        Country GetCountryByCountryCode(string countryCode)
        {
            return DbContext.Countries.FirstOrDefault(c => c.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
