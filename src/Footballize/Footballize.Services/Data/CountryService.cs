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
            => this.countriesRepository = countriesRepository;


        public Task<Country> GetByIdAsync(string id)
        {
            return this.countriesRepository.GetByIdAsync(id);
        }

        public IEnumerable<TViewModel> All<TViewModel>()
        {
            return this.countriesRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>()
                .ToList();
        }

        public async Task UpdateAsync(Country country)
        {
            if (country == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Country)));
            }

            this.countriesRepository.Update(country);
            await this.countriesRepository.SaveChangesAsync();
        }

        public TViewModel GetById<TViewModel>(string id)
        {
            return this.countriesRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefault();
        }

        public Task<string> AddAsync(string name, string isoCode)
        {
            var country = new Country
            {
                Name = name,
                IsoCode = isoCode
            };

            this.countriesRepository.AddAsync(country);
            this.countriesRepository.SaveChangesAsync();

            return Task.FromResult(country.Id);
        }

        public async Task DeleteAsync(string countryId)
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
