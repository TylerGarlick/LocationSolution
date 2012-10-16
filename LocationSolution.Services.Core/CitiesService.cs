using System;
using System.Collections.Generic;
using System.Linq;
using LocationSolution.Data.Core;
using LocationSolution.Repositories.Common;
using LocationSolution.Services.Common;
using NexBusiness.Validation.Core;

namespace LocationSolution.Services.Core
{
    public class CitiesService : ICitiesService
    {
        public ICityRepository CityRepository { get; private set; }

        public CitiesService(ICityRepository cityRepository)
        {
            CityRepository = cityRepository;
        }

        public IEnumerable<City> GetAll()
        {
            return CityRepository.All();
        }

        public City Create(City entity)
        {
            var errors = ValidateCityCreate(entity);
            if (!errors.Any())
            {
                entity = CityRepository.Add(entity);
                return entity;
            }

            throw new ErrorException(errors);
        }

        public City Update(City entity)
        {
            var errors = ValidateCityUpdate(entity);
            if (!errors.Any())
            {
                entity = CityRepository.Update(entity);
                return entity;
            }

            throw new ErrorException(errors);
        }

        public void Delete(City entity)
        {
            try
            {
                CityRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new ErrorException(new[] { new Error(string.Empty, ex.Message) });
            }
        }

        public IEnumerable<City> SearchByCityNameOrNickName(string query)
        {
            return CityRepository.SearchByCityNameOrNickName(query);
        }

        public IEnumerable<City> GetAllCitiesByStateIdAndCountryCode(string countryCode, int stateId)
        {
            return CityRepository.ByCountryCodeAndStateId(countryCode, stateId);
        }

        public City GetCityByCityId(int id)
        {
            return CityRepository.ByKey(c => c.CityId == id);
        }

        public City GetCityByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId)
        {
            return CityRepository.ByCountryCodeAndStateIdAndCityId(countryCode, stateId, cityId);
        }

        IEnumerable<Error> BasicCityValidation(City entity)
        {
            if (entity.State == null)
                yield return new Error("State");

            if (string.IsNullOrEmpty(entity.CityName))
                yield return new Error("CityName", "City Name is required.");

            if (string.IsNullOrEmpty(entity.CityName))
                yield return new Error("CityName", "City Name is required.");
        }

        IEnumerable<Error> ValidateCityCreate(City entity)
        {
            foreach (var error in BasicCityValidation(entity)) yield return error;

            foreach (var error in ValidateUniquenessOfCityName(entity)) yield return error;
        }

        IEnumerable<Error> ValidateCityUpdate(City entity)
        {
            foreach (var error in BasicCityValidation(entity)) yield return error;

            var oldCity = GetCityByCityId(entity.CityId);
            if (!oldCity.CityName.Equals(entity.CityName, StringComparison.InvariantCultureIgnoreCase))
                foreach (var error in ValidateUniquenessOfCityName(entity)) yield return error;
        }

        IEnumerable<Error> ValidateUniquenessOfCityName(City entity)
        {
            if (entity.State.Cities.Any(c => c.CityName.Equals(entity.CityName, StringComparison.InvariantCultureIgnoreCase)))
                yield return new Error("CityName", string.Format("The city name {0} is already used. Please try another", entity.CityName));
        }
    }
}