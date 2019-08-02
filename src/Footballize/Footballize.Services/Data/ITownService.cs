namespace Footballize.Services.Data
{
    using System.Threading.Tasks;
    using Models;

    public interface ITownService
    {
        Task AddTown(Town town);
        TViewModel GetTown<TViewModel>(string id);
        Task DeleteTown(string id);
        Task UpdateTown(Town town);
    }
}