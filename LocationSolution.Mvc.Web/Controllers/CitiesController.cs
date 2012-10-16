using LocationSolution.Data.Core;
using LocationSolution.Mvc.Web.Models;
using LocationSolution.Mvc.Web.Models.Cities;
using LocationSolution.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NexBusiness.Validation.Core;
using PagedList;

namespace LocationSolution.Mvc.Web.Controllers
{
    public class CitiesController : Controller
    {
        public ICountriesService CountriesService { get; private set; }
        public IStatesService StatesService { get; private set; }
        public ICitiesService CitiesService { get; private set; }

        public CitiesController(ICountriesService countriesService, IStatesService statesService, ICitiesService citiesService)
        {
            CountriesService = countriesService;
            StatesService = statesService;
            CitiesService = citiesService;
        }

        public ActionResult Index(string query, int page = 1)
        {
            var cities = string.IsNullOrEmpty(query) ?
                            CitiesService.GetAll() :
                            CitiesService.SearchByCityNameOrNickName(query);
            var model = new CitiesIndexModel() { Cities = cities };
            model.CityResults = new PagedList<City>(model.Cities, page, 50);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CityCreateModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CityCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var country = CountriesService.GetCountryByCountryCode(model.CountryCode);
                    var state = StatesService.GetStateByStateId(model.StateId);
                    if (country != null && state != null)
                    {
                        var city = new City()
                        {
                            CityName = model.CityName,
                            CountyCode = country.CountryCode,
                            NickName = model.NickName,
                            StateId = model.StateId,
                            State = state
                        };

                        city = CitiesService.Create(city);
                        this.FlashInfo(string.Format("City {0} was created successfully", city.CityName));
                        return RedirectToAction("Index");
                    }
                }
                catch (ErrorException errorException)
                {
                    errorException.ToModelState(this);
                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var city = CitiesService.GetCityByCityId(id);
            if (city != null)
            {
                var model = new CityUpdateModel(city);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int id, CityUpdateModel model)
        {
            var city = CitiesService.GetCityByCityId(id);

            if (ModelState.IsValid)
            {
                try
                {
                    city.CountyCode = model.CountryCode;
                    city.StateId = model.StateId;
                    city.CityName = model.CityName;
                    city.NickName = model.NickName;

                    city = CitiesService.Update(city);

                    this.FlashInfo(string.Format("City {0} was updated successfully", city.CityName));
                    return RedirectToAction("Index");
                }
                catch (ErrorException errorException)
                {
                    errorException.ToModelState(this);
                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var city = CitiesService.GetCityByCityId(id);
            if (city != null)
            {
                var model = new CityDeleteModel(city);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id, CityDeleteModel model)
        {
            try
            {
                var city = CitiesService.GetCityByCityId(id);
                CitiesService.Delete(city);
                this.FlashInfo(string.Format("City {0} was deleted successfully!", city.CityName));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
