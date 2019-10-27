namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Footballize.Models;
    using Models.Pitch;

    public interface IPitchService : IService
    {
        Task AddAsync(string name, string addressId);
        IEnumerable<TViewModel> GetAll<TViewModel>();
        IEnumerable<MostUsedPitchServiceModel> GetMostUsed(int count = 3);
        Task<PitchServiceModel> GetByIdAsync(string id);
        TViewModel GetById<TViewModel>(string id);
        Task UpdateAsync(string id, string name, string addressId);
        Task RemoveAsync(string id);
        bool Exist(string name, string addressId);
        IEnumerable<TViewModel> GetByTownId<TViewModel>(string id);
    }
}