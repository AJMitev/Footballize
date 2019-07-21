namespace Footballize.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Footballize.Models;

    public interface ICountryService
    {
        Task<int> AddCountry(Country country);
        void RemoveCountry(Country country);
        Task<Country> GetCountry(string id);
        IEnumerable<Country> GetCountries();
    }
}