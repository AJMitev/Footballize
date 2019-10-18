namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IProvinceService : IService
    {
        IEnumerable<TViewModel> GetProvinces<TViewModel>();
        IEnumerable<TViewModel> GetProvincesByCountry<TViewModel>(string countryId);
        Task CreateProvinceAsync(Province province);
        Task RemoveProvinceAsync(string id);
        TViewModel GetProvince<TViewModel>(string id);
        Task UpdateProvinceAsync(Province province);
    }
}