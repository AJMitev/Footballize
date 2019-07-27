namespace Footballize.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Footballize.Models;

    public interface ICountryService
    {
        int AddCountry(Country country);
        void RemoveCountry(string countryId);
        Country GetCountry(string id);
        IEnumerable<Country> GetCountries();
        void UpdateCountry(Country country);
    }
}