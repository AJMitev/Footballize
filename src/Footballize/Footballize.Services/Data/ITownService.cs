namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITownService
    {
        Task AddTownAsync(Town town);
        TViewModel GetTown<TViewModel>(string id);
        IEnumerable<TViewModel> GetTownsByCountry<TViewModel>(string countryId);
        Task DeleteTownAsync(string id);
        Task UpdateTownAsync(Town town);
    }
}