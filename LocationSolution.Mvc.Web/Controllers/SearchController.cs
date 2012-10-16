using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocationSolution.Mvc.Web.Models.Search;
using LocationSolution.Services.Common;
using PagedList;

namespace LocationSolution.Mvc.Web.Controllers
{
    public class SearchController : Controller
    {
        public ICountriesService CountriesService { get; private set; }
        public IStatesService StatesService { get; private set; }
        public ICitiesService CitiesService { get; private set; }
        public IZipCodesService ZipCodesService { get; private set; }

        public SearchController(ICountriesService countriesService, IStatesService statesService, ICitiesService citiesService, IZipCodesService zipCodesService)
        {
            CountriesService = countriesService;
            StatesService = statesService;
            CitiesService = citiesService;
            ZipCodesService = zipCodesService;
        }

        public ActionResult Index(string q, int page = 1)
        {
            var model = new SearchIndexModel();
            if (!string.IsNullOrEmpty(q))
            {
                foreach (var country in CountriesService.SearchByCountryNameOrCode(q))
                    model.Results.Add(new SearchResult() { Description = "Country", Name = country.CountryName, Url = Url.Action("Edit", "Countries", new { id = country.CountryCode }) });

                foreach (var state in StatesService.SearchByStateNameOrCode(q))
                    model.Results.Add(new SearchResult() { Description = "State", Name = state.StateName, Url = Url.Action("Edit", "States", new { id = state.StateId }) });

                foreach (var city in CitiesService.SearchByCityNameOrNickName(q))
                    model.Results.Add(new SearchResult() { Description = "City", Name = city.CityName, Url = Url.Action("Edit", "Cities", new { id = city.CityId }) });

                foreach (var zipCode in ZipCodesService.SearchByZipcode(q))
                    model.Results.Add(new SearchResult() { Description = "Zip", Name = zipCode.Zip, Url = Url.Action("Edit", "ZipCodes", new { id = zipCode.Zip }) });
            }

            model.PagedResults = new PagedList<SearchResult>(model.Results, page, 25);
            
            return View(model);
        }

    }
}
