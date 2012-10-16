using System;
using System.Collections.Generic;
using System.Linq;
using LocationSolution.Data.Core;
using LocationSolution.Repositories.Common;
using LocationSolution.Services.Common;
using NexBusiness.Validation.Core;

namespace LocationSolution.Services.Core
{
    public class CountriesService : ICountriesService
    {
        public ICountryRepository CountryRepository { get; private set; }

        public CountriesService(ICountryRepository countryRepository)
        {
            CountryRepository = countryRepository;
        }

        public IEnumerable<Country> GetAll()
        {
            return CountryRepository.All();
        }

        public Country Create(Country entity)
        {
            var errors = ValidateCountryCreate(entity);
            if (!errors.Any())
            {
                entity = CountryRepository.Add(entity);
                return entity;
            }
            throw new ErrorException(errors);
        }

        public Country Update(Country entity)
        {
            var errors = ValidateCountryUpdate(entity);
            if (!errors.Any())
            {
                entity = CountryRepository.Update(entity);
                return entity;
            }

            throw new ErrorException(errors);
        }

        public void Delete(Country entity)
        {
            try
            {
                CountryRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new ErrorException(new[] { new Error(string.Empty, ex.Message) });
            }
        }

        public IEnumerable<Country> GetAllActiveCountries()
        {
            return CountryRepository.AllActive();
        }

        public IEnumerable<Country> SearchByCountryNameOrCode(string query)
        {
            return CountryRepository.SearchByCountryCodeOrName(query);
        }

        public Country GetCountryByCountryCode(string countryCode)
        {
            var country = CountryRepository.ByKey(c => c.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase));
            if (country != null)
                return country;

            throw new KeyNotFoundException(string.Format("Country Code {0} was not found.", countryCode));
        }

        IEnumerable<Error> BasicCountryValidation(Country entity)
        {
            if (string.IsNullOrEmpty(entity.CountryCode))
                yield return new Error("CountryCode", "Country Code is required.");

            if (string.IsNullOrEmpty(entity.CountryName))
                yield return new Error("CountryName", "Country Name is required.");

            if (string.IsNullOrEmpty(entity.Displayable))
                yield return new Error("Displayable", "Displayable is required.");
        }

        IEnumerable<Error> ValidateCountryUpdate(Country entity)
        {
            var originalEntity = GetCountryByCountryCode(entity.CountryCode);
            foreach (var error in BasicCountryValidation(entity)) yield return error;

            // We changed the country code.
            if (!originalEntity.CountryCode.Equals(entity.CountryCode))
                foreach (var error in ValidateUniquenessOfCountryCode(entity)) yield return error;

            // We changed the country name.
            if (!originalEntity.CountryName.Equals(entity.CountryName))
                foreach (var error in ValidateUniquenessOfCountryName(entity)) yield return error;
        }

        IEnumerable<Error> ValidateCountryCreate(Country entity)
        {
            foreach (var error in BasicCountryValidation(entity)) yield return error;

            foreach (var error in ValidateUniquenessOfCountryName(entity)) yield return error;

            foreach (var error in ValidateUniquenessOfCountryCode(entity)) yield return error;
        }

        IEnumerable<Error> ValidateUniquenessOfCountryCode(Country entity)
        {
            if (GetAll().Any(c => c.CountryCode.Equals(entity.CountryCode, StringComparison.InvariantCultureIgnoreCase)))
                yield return new Error("CountryName", string.Format("There is a country with the code {0} already.  Please choose another code.", entity.CountryCode));
        }

        IEnumerable<Error> ValidateUniquenessOfCountryName(Country entity)
        {
            if (GetAll().Any(c => c.CountryName.Equals(entity.CountryName, StringComparison.InvariantCultureIgnoreCase)))
                yield return new Error("CountryName", string.Format("There is a country with the name {0} already.  Please choose another name.", entity.CountryName));
        }
    }
}
