namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IGatherServices
    {
        ICollection<TViewModel> GetGathers<TViewModel>();
        Task AddGatherAsync(Gather country);
    }
}