namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IProvinceServices
    {
        IEnumerable<TViewModel> GetProvinces<TViewModel>();
        Task CreateProvince(Province province);
    }
}