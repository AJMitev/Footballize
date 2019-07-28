namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICountryService
    {
        Task<int> AddCountry(Country country);
        Task RemoveCountry(string countryId);
        TViewModel GetCountry<TViewModel>(string id);
        Task<Country> GetCountryById(string id);
        IEnumerable<TViewModel> GetCountries<TViewModel>();
        Task UpdateCountry(Country country);
    }
}