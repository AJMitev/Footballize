namespace Footballize.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Data.Repositories;
    using Footballize.Models;
    using Mapping;
    using Models.Country;

    public class CountryService : ICountryService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountryService(IDeletableEntityRepository<Country> countriesRepository)
            => this.countriesRepository = countriesRepository;


        public CountryServiceModel GetByIdAsync(string id) 
            => this.countriesRepository
                .All()
                .Where(x => x.Id == id)
                .To<CountryServiceModel>()
                .SingleOrDefault();

        public IEnumerable<TViewModel> All<TViewModel>() 
            => this.countriesRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>()
                .ToList();

        public async Task UpdateAsync(string id, string name, string isoCode)
        {
            var country = await this.countriesRepository.GetByIdAsync(id);

            country.Name = name;
            country.IsoCode = isoCode;

            this.countriesRepository.Update(country);
            await this.countriesRepository.SaveChangesAsync();
        }

        public TViewModel GetById<TViewModel>(string id) 
            => this.countriesRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefault();

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

        public async Task DeleteAsync(string id)
        {
            var countryToRemove = await this.countriesRepository.GetByIdAsync(id);

            if (countryToRemove == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Country)));
            }

            this.countriesRepository.Delete(countryToRemove);
            await this.countriesRepository.SaveChangesAsync();
        }
    }
}
