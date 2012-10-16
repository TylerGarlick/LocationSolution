using System;
using System.Collections.Generic;
using System.Linq;
using LocationSolution.Data.Core;
using LocationSolution.Repositories.Common;
using LocationSolution.Services.Common;
using NexBusiness.Validation.Core;

namespace LocationSolution.Services.Core
{
    public class ZipCodesService : IZipCodesService
    {
        public IZipCodeRepository ZipCodeRepository { get; private set; }

        public ZipCodesService(IZipCodeRepository zipCodeRepository)
        {
            ZipCodeRepository = zipCodeRepository;
        }

        public IEnumerable<ZipCode> GetAll()
        {
            return ZipCodeRepository.All();
        }

        public ZipCode Create(ZipCode entity)
        {
            var errors = ValidationZipCodeCreate(entity);
            if (!errors.Any())
            {
                entity = ZipCodeRepository.Add(entity);
                return entity;
            }

            throw new ErrorException(errors);
        }

        public ZipCode Update(ZipCode entity)
        {
            var errors = ValidationZipCodeUpdate(entity);
            if (!errors.Any())
            {
                entity = ZipCodeRepository.Update(entity);
                return entity;
            }

            throw new ErrorException(errors);
        }

        public void Delete(ZipCode entity)
        {
            try
            {
                ZipCodeRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new ErrorException(new[] { new Error(string.Empty, ex.Message) });
            }
        }

        public IEnumerable<ZipCode> SearchByZipcode(string query)
        {
            return ZipCodeRepository.SearchByZipCode(query);
        }

        public IEnumerable<ZipCode> GetAllZipsByCountryCodeAndStateIdAndCityId(string countryCode, int stateId, int cityId)
        {
            return ZipCodeRepository.ByCountryCodeAndStateIdAndCityId(countryCode, stateId, cityId);
        }

        public ZipCode GetByZipCode(string zipCode)
        {
            return ZipCodeRepository.ByKey(z => z.Zip.Equals(zipCode, StringComparison.InvariantCultureIgnoreCase));
        }

        public ZipCode GetByCountryCodeAndStateIdAndCityIdAndZipCode(string countryCode, int stateId, int cityId, string zipCode)
        {
            return GetAllZipsByCountryCodeAndStateIdAndCityId(countryCode, stateId, cityId).FirstOrDefault(z => z.Zip.Equals(zipCode, StringComparison.InvariantCultureIgnoreCase));
        }

        IEnumerable<Error> BasicZipCodeValidation(ZipCode entity)
        {
            if (entity.City == null)
                yield return new Error("City");

            if (string.IsNullOrEmpty(entity.Zip))
                yield return new Error("Zip", "Zip is required.");
        }

        IEnumerable<Error> ValidationZipCodeCreate(ZipCode entity)
        {
            foreach (var error in BasicZipCodeValidation(entity)) yield return error;

            foreach (var error in ValidateUniquenessOfZipCodeInCity(entity)) yield return error;
        }

        IEnumerable<Error> ValidationZipCodeUpdate(ZipCode entity)
        {
            foreach (var error in BasicZipCodeValidation(entity)) yield return error;

            var oldZip = ZipCodeRepository.ByCountryCodeAndStateIdAndCityIdAndZipCode(entity.CountyCode, entity.StateId, entity.CityId, entity.Zip);
            if (!oldZip.Zip.Equals(entity.Zip))
                foreach (var error in ValidateUniquenessOfZipCodeInCity(entity)) yield return error;
        }

        IEnumerable<Error> ValidateUniquenessOfZipCodeInCity(ZipCode entity)
        {
            if (entity.City.ZipCodes.Any(z => z.Zip.Equals(entity.Zip, StringComparison.InvariantCultureIgnoreCase)))
                yield return new Error("Zip", string.Format("Zip {0} already exists.  Please try again.", entity.Zip));
        }
    }
}