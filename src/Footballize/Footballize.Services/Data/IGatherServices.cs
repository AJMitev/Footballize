namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IGatherServices
    {
        ICollection<TViewModel> GetGathers<TViewModel>();
        TViewModel GetGather<TViewModel>(string id);
        Task<Gather> GetGatherAsync(string id);
        Gather GetGatherWithPlayers(string id);
        Task AddGatherAsync(Gather gather);
        Task LeaveGatherAsync(Gather gather, string userId);
        Task EnrollGatherAsync(Gather gather, User user);
        Task StartGatherAsync(string id);
        Task CompleteGatherAsync(string id);
        Task DeleteGatherAsync(string id);
        int GetGatherCount();
    }
}