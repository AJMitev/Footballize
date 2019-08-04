namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IPlayfieldService
    {
        Task AddPlayfiledAsync(Playfield playfield);
        IEnumerable<TViewModel> GetPlayfileds<TViewModel>();
        TViewModel GetPlayfiled<TViewModel>(string id);
        Task<Playfield> GetPlayfiledAsync(string id);
        Task UpdatePlayfieldAsync(Playfield playfield);
        Task RemovePlayfieldAsync(Playfield playfield);
    }
}