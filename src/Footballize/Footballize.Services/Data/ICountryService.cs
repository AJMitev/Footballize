namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICountryService : IService
    {
        Task<string> AddAsync(string name, string isoCode);
        Task DeleteAsync(string countryId);
        TViewModel GetById<TViewModel>(string id);
        //TODO: This should return DTO
        Task<Country> GetByIdAsync(string id);
        IEnumerable<TViewModel> All<TViewModel>();
        Task UpdateAsync(Country country);
    }
}