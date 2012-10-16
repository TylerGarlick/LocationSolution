using LocationSolution.Data.Core;
using Ninject.Modules;
using System.Data.Entity;
using LocationSolution.Repositories.Common;

namespace LocationSolution.Repositories.EntityFramework.Configuration.Ninject
{
    public class RepositoryNinjectModule : NinjectModule

    {
        public override void Load()
        {
            Bind<DbContext>().To<LocationsEntities>().InThreadScope();

            Bind<ICountryRepository>().To<CountryRepository>();
            Bind<IStateRepository>().To<StateRepository>();
            Bind<ICityRepository>().To<CityRepository>();
            Bind<IZipCodeRepository>().To<ZipCodeRepository>();
        }
    }
}
