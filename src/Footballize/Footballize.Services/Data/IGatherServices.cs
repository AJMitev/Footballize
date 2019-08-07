namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IGatherServices
    {
        ICollection<TViewModel> GetGathers<TViewModel>();
        TViewModel GetGather<TViewModel>(string id);
        Task AddGatherAsync(Gather gather);
        Task LeaveGatherAsync(string gatherId, string userId);
        Task EnrollGatherAsync(string gatherId, string userId);
        Task StartGather(string id);
    }
}