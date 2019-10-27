namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProvinceService : IService
    {
        IEnumerable<TViewModel> GetAll<TViewModel>();
        IEnumerable<TViewModel> GetAllByCountry<TViewModel>(string id);
        Task AddAsync(string name, string countryId);
        Task RemoveAsync(string id);
        TViewModel GetById<TViewModel>(string id);
        Task UpdateAsync(string id, string name, string countryId);
    }
}