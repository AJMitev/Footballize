﻿namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data;
    using Footballize.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CountryService : ICountryService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountryService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public Task<int> AddCountry(Country country)
        {
            this.countriesRepository.AddAsync(country);
            return this.countriesRepository.SaveChangesAsync();
        }

        public Task<Country> GetCountryById(string id)
        {
            return this.countriesRepository.GetByIdAsync(id);
        }

        public IEnumerable<TViewModel> GetCountries<TViewModel>()
        {
            return this.countriesRepository
                .All()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .To<TViewModel>()
                .ToList();
        }

        public async Task UpdateCountry(Country country)
        {
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

        public async Task RemoveCountry(string countryId)
        {
            var countryToRemove = await this.countriesRepository.GetByIdAsync(countryId);
            this.countriesRepository.Delete(countryToRemove);
            await this.countriesRepository.SaveChangesAsync();
        }
    }
}