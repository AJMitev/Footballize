namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class PlayfieldService : IPlayfieldService
    {
        private readonly IRepository<Playfield> playfieldRepository;

        public PlayfieldService(IRepository<Playfield> playfieldRepository)
        {
            this.playfieldRepository = playfieldRepository;
        }

        public async Task AddPlayfiledAsync(Playfield playfield)
        {
            await this.playfieldRepository.AddAsync(playfield);
            await this.playfieldRepository.SaveChangesAsync();
        }

        public TViewModel GetPlayfiled<TViewModel>(string id) => this.playfieldRepository
                .All()
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();

        public async Task<Playfield> GetPlayfiledAsync(string id) => await this.playfieldRepository.GetByIdAsync(id);

        public IEnumerable<TViewModel> GetPlayfileds<TViewModel>() => this.playfieldRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>();

        public async Task RemovePlayfieldAsync(Playfield playfield)
        {
            this.playfieldRepository.Delete(playfield);
            await this.playfieldRepository.SaveChangesAsync();
        }

        public async Task UpdatePlayfieldAsync(Playfield playfield)
        {
            this.playfieldRepository.Update(playfield);
            await this.playfieldRepository.SaveChangesAsync();
        }
    }
}