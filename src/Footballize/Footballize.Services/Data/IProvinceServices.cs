namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IProvinceServices
    {
        IEnumerable<TViewModel> GetProvinces<TViewModel>();
        Task CreateProvince(Province province);
        Task RemoveProvince(string id);
        TViewModel GetProvince<TViewModel>(string id);
        Task UpdateProvince(Province province);
    }
}