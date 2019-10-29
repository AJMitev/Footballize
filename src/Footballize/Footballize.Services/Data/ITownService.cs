namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITownService : IService
    {
        Task AddTownAsync(string name, string provinceId);
        TViewModel GetById<TViewModel>(string id);
        IEnumerable<TViewModel> GetByCountryId<TViewModel>(string countryId);
        Task DeleteAsync(string id);
        Task UpdateAsync(string townId, string name, string provinceId);
    }
}