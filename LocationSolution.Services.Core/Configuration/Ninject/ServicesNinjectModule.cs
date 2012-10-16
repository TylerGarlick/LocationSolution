using LocationSolution.Services.Common;
using Ninject.Modules;

namespace LocationSolution.Services.Core.Configuration.Ninject
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICountriesService>().To<CountriesService>();
            Bind<IStatesService>().To<StatesService>();
            Bind<ICitiesService>().To<CitiesService>();
            Bind<IZipCodesService>().To<ZipCodesService>();
        }
    }
}
