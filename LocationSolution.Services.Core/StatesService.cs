using System;
using System.Collections.Generic;
using System.Linq;
using LocationSolution.Data.Core;
using LocationSolution.Repositories.Common;
using LocationSolution.Services.Common;
using NexBusiness.Validation.Core;

namespace LocationSolution.Services.Core
{
    public class StatesService : IStatesService
    {
        public IStateRepository StateRepository { get; private set; }

        public StatesService(IStateRepository stateRepository)
        {
            StateRepository = stateRepository;
        }

        public IEnumerable<State> GetAll()
        {
            return StateRepository.All();
        }

        public State Create(State entity)
        {
            var errors = ValidateStateCreate(entity);
            if (!errors.Any())
            {
                entity = StateRepository.Add(entity);
                return entity;
            }

            throw new ErrorException(errors);
        }

        public State Update(State entity)
        {
            var errors = ValidateStateUpdate(entity);
            if (!errors.Any())
            {
                entity = StateRepository.Update(entity);
                return entity;
            }

            throw new ErrorException(errors);
        }

        public void Delete(State entity)
        {
            try
            {
                StateRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new ErrorException(new[] { new Error(string.Empty, ex.Message) });
            }
        }

        public IEnumerable<State> SearchByStateNameOrCode(string query)
        {
            var loweredQuery = query.ToLower();
            return GetAll().Where(s => s.StateName.ToLower().Contains(loweredQuery) || s.StateCode.ToLower().Contains(loweredQuery) || s.Country.CountryName.ToLower().Contains(loweredQuery) || s.Country.CountryCode.ToLower().Contains(loweredQuery));
        }

        public IEnumerable<State> GetAllStatesByCountryCode(string countryCode)
        {
            return StateRepository.ByCountryCode(countryCode);
        }

        public State GetStateByStateId(int id)
        {
            return StateRepository.ByKey(u => u.StateId == id);
        }

        public State GetStateByCountryCodeAndStateId(string countryCode, int id)
        {
            return StateRepository.ByKey(u => u.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase) && u.StateId == id);
        }

        IEnumerable<Error> BasicStateValidation(State entity)
        {
            if (entity.Country == null)
                yield return new Error("Country", "Country is required.");

            if (string.IsNullOrEmpty(entity.StateCode))
                yield return new Error("StateCode", "State Code is required.");

            if (string.IsNullOrEmpty(entity.StateName))
                yield return new Error("StateName", "State Name is required.");

            if (string.IsNullOrEmpty(entity.ContiguousLand))
                yield return new Error("ContiguousLand", "Contiguous Land is required.");

            if (string.IsNullOrEmpty(entity.IsTerritory))
                yield return new Error("IsTerritory", "Is State Territory is required.");
        }

        IEnumerable<Error> ValidateStateCreate(State entity)
        {
            foreach (var error in BasicStateValidation(entity)) yield return error;

            if (entity.Country != null)
            {
                foreach (var error in ValidateUniquenessOfStateName(entity)) yield return error;

                foreach (var error in ValidateUniquenessOfStateCode(entity)) yield return error;
            }
        }

        IEnumerable<Error> ValidateStateUpdate(State entity)
        {
            foreach (var error in BasicStateValidation(entity)) yield return error;

            var oldState = GetStateByStateId(entity.StateId);

            if (entity.Country == null)
            {
                if (!oldState.StateName.Equals(entity.StateName, StringComparison.InvariantCultureIgnoreCase))
                    foreach (var error in ValidateUniquenessOfStateName(entity)) yield return error;

                if (!oldState.StateCode.Equals(entity.StateCode, StringComparison.InvariantCultureIgnoreCase))
                    foreach (var error in ValidateUniquenessOfStateCode(entity)) yield return error;
            }
        }

        IEnumerable<Error> ValidateUniquenessOfStateName(State entity)
        {
            if (entity.Country.States.Any(s => s.StateName.Equals(entity.StateName, StringComparison.InvariantCultureIgnoreCase)))
                yield return new Error("StateName", string.Format("There is already a state with the name {0}, please try again.", entity.StateName));
        }

        IEnumerable<Error> ValidateUniquenessOfStateCode(State entity)
        {
            if (entity.Country.States.Any(s => s.StateCode.Equals(entity.StateCode, StringComparison.InvariantCultureIgnoreCase)))
                yield return new Error("StateCode", string.Format("There is already a state with the code {0}, please try again.", entity.StateCode));
        }
    }
}