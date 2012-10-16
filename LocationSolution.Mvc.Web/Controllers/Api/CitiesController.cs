using LocationSolution.Mvc.Web.Models.Api.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LocationSolution.Services.Common;

namespace LocationSolution.Mvc.Web.Controllers.Api
{
    public class CitiesController : ApiController
    {
        public IStatesService StatesService { get; private set; }

        public CitiesController(IStatesService statesService)
        {
            StatesService = statesService;
        }

        // GET api/cities
        public IEnumerable<CityDto> Get(int stateId)
        {
            var state = StatesService.GetStateByStateId(stateId);
            if (state != null)
            {
                return state.Cities.OrderBy(c => c.CityName).Select(c => new CityDto() { CityName = c.CityName, CityId = c.CityId, StateId = c.StateId });
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // POST api/cities
        public void Post([FromBody]string value)
        {
        }

        // PUT api/cities/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/cities/5
        public void Delete(int id)
        {
        }
    }
}
