namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICountryService : IService
    {
        Task<int> AddCountryAsync(Country country);
        Task RemoveCountryAsync(string countryId);
        TViewModel GetCountry<TViewModel>(string id);
        Task<Country> GetCountryByIdAsync(string id);
        IEnumerable<TViewModel> GetCountries<TViewModel>();
        Task UpdateCountryAsync(Country country);
    }
}