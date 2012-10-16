using LocationSolution.Data.Core;
using LocationSolution.Mvc.Web.Models.Api.Countries;
using LocationSolution.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LocationSolution.Mvc.Web.Controllers.Api
{
    public class CountriesController : ApiController
    {
        public ICountriesService CountriesService { get; private set; }

        public CountriesController(ICountriesService countriesService)
        {
            CountriesService = countriesService;
        }

        // GET api/countries
        public IEnumerable<CountryDto> Get()
        {
            return CountriesService.GetAllActiveCountries().Select(c => new CountryDto(){CountryCode = c.CountryCode, CountryName = c.CountryName});
        }

        // GET api/countries/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/countries
        public void Post([FromBody]string value)
        {
        }

        // PUT api/countries/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/countries/5
        public void Delete(int id)
        {
        }
    }
}
