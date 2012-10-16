using System;
using System.Linq;
using System.Web.Mvc;
using LocationSolution.Data.Core;
using LocationSolution.Mvc.Web.Models.Countries;
using LocationSolution.Services.Common;
using NexBusiness.Validation.Core;

namespace LocationSolution.Mvc.Web.Controllers
{
    public class CountriesController : Controller
    {
        public ICountriesService CountriesService { get; private set; }

        public CountriesController(ICountriesService countriesService)
        {
            CountriesService = countriesService;
        }

        public ActionResult Index(string query)
        {
            var countries = string.IsNullOrEmpty(query) ?
                                CountriesService.GetAll() :
                                CountriesService.SearchByCountryNameOrCode(query);

            var model = new CountriesIndexModel() { Countries = countries };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var numberOfCountries = CountriesService.GetAll().Count();
            var model = new CountryCreateModel(numberOfCountries);
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(CountryCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var country = new Country()
                                      {
                                          CountryCode = model.CountryCode,
                                          CountryOrder = model.CountryOrder,
                                          CountryName = model.CountryName,
                                          Displayable = model.Displayable ? "1" : "0"
                                      };
                    country = CountriesService.Create(country);
                    this.FlashInfo(string.Format("Country {0} was created succcessfully", country.CountryName));
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

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var country = CountriesService.GetCountryByCountryCode(id);
            if (country != null)
            {
                var model = new CountryUpdateModel(country);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(string id, CountryUpdateModel model)
        {
            var country = CountriesService.GetCountryByCountryCode(id);

            if (ModelState.IsValid)
            {
                try
                {
                    country.CountryCode = model.CountryCode;
                    country.CountryName = model.CountryName;
                    country.CountryOrder = model.CountryOrder;
                    country.Displayable = model.Displayable ? "1" : "0";

                    country = CountriesService.Update(country);

                    //Todo: Flash Update
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

        public ActionResult Delete(string id)
        {
            var country = CountriesService.GetCountryByCountryCode(id);
            if (country != null)
            {
                var model = new CountryDeleteModel(country);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(string id, CountryDeleteModel model)
        {
            try
            {
                var country = CountriesService.GetCountryByCountryCode(id);
                CountriesService.Delete(country);
                this.FlashInfo(string.Format("Country {0} was deleted successfully!", country.CountryName));
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
