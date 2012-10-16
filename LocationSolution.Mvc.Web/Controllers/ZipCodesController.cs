using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocationSolution.Data.Core;
using LocationSolution.Mvc.Web.Models.ZipCodes;
using LocationSolution.Services.Common;
using NexBusiness.Validation.Core;

namespace LocationSolution.Mvc.Web.Controllers
{
    public class ZipCodesController : Controller
    {
        public ICountriesService CountriesService { get; private set; }
        public IStatesService StatesService { get; private set; }
        public ICitiesService CitiesService { get; private set; }
        public IZipCodesService ZipCodesService { get; private set; }

        public ZipCodesController(ICountriesService countriesService, IStatesService statesService, ICitiesService citiesService, IZipCodesService zipCodesService)
        {
            CountriesService = countriesService;
            StatesService = statesService;
            CitiesService = citiesService;
            ZipCodesService = zipCodesService;
        }

        public ActionResult Index(string query)
        {
            var zipCodes = string.IsNullOrEmpty(query) ?
                                ZipCodesService.GetAll() :
                                ZipCodesService.SearchByZipcode(query);
            var model = new ZipCodesIndexModel() { ZipCodes = zipCodes };
            return View(model);
        }


        public ActionResult Create()
        {
            var model = new ZipCodeCreateModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ZipCodeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var country = CountriesService.GetCountryByCountryCode(model.CountryCode);
                    var state = StatesService.GetStateByStateId(model.StateId);
                    var city = CitiesService.GetCityByCityId(model.CityId);
                    if (country != null && state != null && city != null)
                    {
                        var zip = new ZipCode()
                        {
                            City = city,
                            CityId = model.CityId,
                            CountyCode = model.CountryCode,
                            StateId = model.StateId,
                            Zip = model.Zip,
                            Latitude = model.Latitude,
                            Longitude = model.Longitude
                        };

                        zip = ZipCodesService.Create(zip);
                        this.FlashInfo(string.Format("Zip {0} was created successfully", zip.Zip));
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

        public ActionResult Edit(string id)
        {
            var zipCode = ZipCodesService.GetByZipCode(id);
            if (zipCode != null)
            {
                var model = new ZipCodeUpdateModel(zipCode);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(string id, ZipCodeUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var zipCode = ZipCodesService.GetByZipCode(id);
                    var country = CountriesService.GetCountryByCountryCode(model.CountryCode);
                    var state = StatesService.GetStateByStateId(model.StateId);
                    var city = CitiesService.GetCityByCityId(model.CityId);
                    
                    ZipCodesService.Delete(zipCode);

                    if (country != null && state != null && city != null)
                    {
                        var zip = new ZipCode()
                        {
                            City = city,
                            CityId = model.CityId,
                            CountyCode = model.CountryCode,
                            StateId = model.StateId,
                            Zip = model.Zip,
                            Latitude = model.Latitude,
                            Longitude = model.Longitude
                        };

                        zip = ZipCodesService.Create(zip);
                        this.FlashInfo(string.Format("Zip {0} was updated successfully", zip.Zip));
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

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var zipCode = ZipCodesService.GetByZipCode(id);
            if (zipCode != null)
            {
                var model = new ZipCodeDeleteModel(zipCode);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(string id, ZipCodeDeleteModel model)
        {
            try
            {
                var zipCode = ZipCodesService.GetByZipCode(id);
                ZipCodesService.Delete(zipCode);
                this.FlashInfo(string.Format("Zip Code {0} was deleted successfully!", zipCode.Zip));
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
