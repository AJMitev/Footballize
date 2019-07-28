namespace Footballize.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CountryService : ICountryService
    {
        private readonly FootballizeDbContext dbContext;

        public CountryService(FootballizeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> AddCountry(Country country)
        {
            this.dbContext.Countries.AddAsync(country);
            return this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<Country> GetCountries()
        {
            return this.dbContext.Countries
                .Where(x=>!x.IsDeleted)
                .OrderBy(x=>x.Name)
                .ToList();
        }

        public void UpdateCountry(Country country)
        {

            var entry = this.dbContext.Entry(country);
            if (entry.State == EntityState.Detached)
            {
                this.dbContext.Attach(country);
            }

            entry.State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }

        public Country GetCountry(string id)
        {
            return  this.dbContext.Countries.Find(id);
        }

        public void RemoveCountry(string countryId)
        {
            var countryToRemove =  this.dbContext.Find<Country>(countryId);
            countryToRemove.IsDeleted = true;
            countryToRemove.DeletedOn = DateTime.UtcNow;

            this.dbContext.Countries.Update(countryToRemove);
            this.dbContext.SaveChanges();
        }
    }
}
