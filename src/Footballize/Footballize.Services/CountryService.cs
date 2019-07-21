namespace Footballize.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Models;

    public class CountryService : ICountryService
    {
        private readonly FootballizeDbContext dbContext;

        public CountryService(FootballizeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddCountry(Country country)
        {
           await this.dbContext.Countries.AddAsync(country);
           return await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<Country> GetCountries()
        {
            return this.dbContext.Countries.ToList();
        }

        public async Task<Country> GetCountry(string id)
        {
            return await this.dbContext.Countries.FindAsync(id);
        }

        public void RemoveCountry(Country country)
        {
            this.dbContext.Countries.Remove(country);
            this.dbContext.SaveChanges();
        }
    }
}
