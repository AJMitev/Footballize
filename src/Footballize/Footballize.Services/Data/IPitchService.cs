namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IPitchService
    {
        Task AddPitchAsync(Pitch pitch);
        IEnumerable<TViewModel> GetPitches<TViewModel>();
        TViewModel GetPitch<TViewModel>(string id);
        Task<Pitch> GetPitchAsync(string id);
        Task UpdatePitchAsync(Pitch pitch);
        Task RemovePitchAsync(Pitch pitch);
    }
}