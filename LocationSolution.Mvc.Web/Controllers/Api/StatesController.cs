using LocationSolution.Mvc.Web.Models.Api.States;
using LocationSolution.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LocationSolution.Mvc.Web.Controllers.Api
{
    public class StatesController : ApiController
    {
        public ICountriesService CountriesService { get; private set; }
        public IStatesService StatesService { get; private set; }

        public StatesController(ICountriesService countriesService, IStatesService statesService)
        {
            CountriesService = countriesService;
            StatesService = statesService;
        }

        public IEnumerable<StateDto> Get(string countryCode)
        {
            var country = CountriesService.GetCountryByCountryCode(countryCode);
            if (country != null)
            {
                return country.States.OrderBy(s => s.StateName).Select(s => new StateDto() { CountryCode = s.CountryCode, StateId = s.StateId, StateName = s.StateName });
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // GET api/states/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/states
        public void Post([FromBody]string value)
        {
        }

        // PUT api/states/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/states/5
        public void Delete(int id)
        {
        }
    }
}
