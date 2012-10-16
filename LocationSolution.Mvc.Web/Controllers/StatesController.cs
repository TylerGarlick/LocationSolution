using System;
using LocationSolution.Data.Core;
using LocationSolution.Mvc.Web.Models;
using LocationSolution.Mvc.Web.Models.States;
using LocationSolution.Services.Common;
using System.Web.Mvc;
using NexBusiness.Validation.Core;

namespace LocationSolution.Mvc.Web.Controllers
{
    public class StatesController : Controller
    {
        public ICountriesService CountriesService { get; private set; }
        public IStatesService StatesService { get; private set; }

        public StatesController(ICountriesService countriesService, IStatesService statesService)
        {
            CountriesService = countriesService;
            StatesService = statesService;
        }

        public ActionResult Index(string query)
        {
            var states = string.IsNullOrEmpty(query) ?
                            StatesService.GetAll() :
                            StatesService.SearchByStateNameOrCode(query);
            var model = new StatesIndexModel() { States = states };
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new StateCreateModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StateCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var country = CountriesService.GetCountryByCountryCode(model.CountryCode);
                    if (country != null)
                    {
                        var state = new State()
                                        {
                                            ContiguousLand = model.ContiguousLand ? "1" : "0",
                                            CountryCode = country.CountryCode,
                                            Country = country,
                                            IsTerritory = model.IsTerritory ? "1" : "0",
                                            StateCode = model.StateCode,
                                            StateName = model.StateName
                                        };
                        state = StatesService.Create(state);
                        this.FlashInfo(string.Format("State {0} was created successfully", state.StateName));
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
            var state = StatesService.GetStateByStateId(id);
            if (state != null)
            {
                var model = new StateUpdateModel(state);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int id, StateUpdateModel model)
        {
            var state = StatesService.GetStateByStateId(id);

            if (ModelState.IsValid)
            {
                try
                {
                    state.StateCode = model.StateCode;
                    state.StateName = model.StateName;
                    state.ContiguousLand = model.ContiguousLand ? "1" : "0";
                    state.IsTerritory = model.IsTerritory ? "1" : "0";

                    state = StatesService.Update(state);

                    this.FlashInfo(string.Format("State {0} was updated successfully", state.StateName));
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
            var state = StatesService.GetStateByStateId(id);
            if (state != null)
            {
                var model = new StateDeleteModel(state);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id, StateDeleteModel model)
        {
            try
            {
                var state = StatesService.GetStateByStateId(id);
                StatesService.Delete(state);

                this.FlashInfo(string.Format("State {0} was deleted successfully!", state.StateName));
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
