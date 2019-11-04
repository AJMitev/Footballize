namespace Footballize.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Country;

    public interface ICountryService : IService
    {
        Task<string> AddAsync(string name, string isoCode);
        Task DeleteAsync(string id);
        TViewModel GetById<TViewModel>(string id);
        CountryServiceModel GetByIdAsync(string id);
        IEnumerable<TViewModel> All<TViewModel>();
        Task UpdateAsync(string id, string name, string isoCode);
    }
}