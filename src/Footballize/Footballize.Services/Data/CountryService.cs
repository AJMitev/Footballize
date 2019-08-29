namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class CountryService : ICountryService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountryService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public Task<int> AddCountryAsync(Country country)
        {
            if (country == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Country)));
            }

            this.countriesRepository.AddAsync(country);
            return this.countriesRepository.SaveChangesAsync();
        }

        public Task<Country> GetCountryByIdAsync(string id)
        {
            return this.countriesRepository.GetByIdAsync(id);
        }

        public IEnumerable<TViewModel> GetCountries<TViewModel>()
        {
            return this.countriesRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>()
                .ToList();
        }

        public async Task UpdateCountryAsync(Country country)
        {
            if (country == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Country)));
            }

            this.countriesRepository.Update(country);
            await this.countriesRepository.SaveChangesAsync();
        }

        public TViewModel GetCountry<TViewModel>(string id)
        {
            return this.countriesRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefault();
        }

        public async Task RemoveCountryAsync(string countryId)
        {
            var countryToRemove = await this.countriesRepository.GetByIdAsync(countryId);

            if (countryToRemove == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Country)));
            }

            this.countriesRepository.Delete(countryToRemove);
            await this.countriesRepository.SaveChangesAsync();
        }
    }
}
