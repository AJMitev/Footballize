namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTOs;
    using Models;

    public interface IPitchService
    {
        Task AddPitchAsync(Pitch pitch);
        IEnumerable<TViewModel> GetPitches<TViewModel>();
        IEnumerable<MostUsedPitchDTO> GetMostUsedPitches(int count = 3);
        TViewModel GetPitch<TViewModel>(string id);
        Task UpdatePitchAsync(Pitch pitch);
        Task RemovePitchAsync(Pitch pitch);
        IEnumerable<TViewModel> GetPitchesByTownId<TViewModel>(string id);
    }
}