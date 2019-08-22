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
        Task AddGatherAsync(Gather gather);
        Task LeaveGatherAsync(Gather gather, string userId);
        Task EnrollGatherAsync(Gather gather, User user);
        Task StartGather(string id);
        Task CompleteGather(string id);
        Task DeleteGatherAsync(string id);
    }
}