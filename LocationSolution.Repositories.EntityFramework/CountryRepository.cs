using System.Data.Entity;
using System.Linq;
using LocationSolution.Data.Core;
using LocationSolution.Repositories.Common;
using NexBusiness.Repositories.EntityFramework;

namespace LocationSolution.Repositories.EntityFramework
{
    public class CountryRepository : Repository<Country, LocationsEntities>, ICountryRepository
    {
        public CountryRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public override IQueryable<Country> All()
        {
            return DbContext.Countries.OrderBy(c => c.CountryOrder);
        }

        public IQueryable<Country> SearchByCountryCodeOrName(string query)
        {
            var loweredQuery = query.ToLower();
            return All().Where(c => c.CountryCode.ToLower().Contains(loweredQuery) || c.CountryName.ToLower().Contains(loweredQuery));
        }

        public IQueryable<Country> AllActive()
        {
            // I really think this should be a bit field, would suggest a data migration to change, but don't know if the previous schema would be impacted
            // Will assume that 1 is displayable and 0 is not.
            return All().Where(c => c.Displayable == "1");
        }
    }
}
