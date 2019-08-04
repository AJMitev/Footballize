namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITownService
    {
        Task AddTown(Town town);
        TViewModel GetTown<TViewModel>(string id);
        IEnumerable<TViewModel> GetTownsByProvince<TViewModel>(string countryId);
        Task DeleteTown(string id);
        Task UpdateTown(Town town);
    }
}