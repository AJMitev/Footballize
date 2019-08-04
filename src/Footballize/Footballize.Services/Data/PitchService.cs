namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class PitchService : IPitchService
    {
        private readonly IRepository<Pitch> _pitchRepository;

        public PitchService(IRepository<Pitch> pitchRepository)
        {
            this._pitchRepository = pitchRepository;
        }

        public async Task AddPitchAsync(Pitch pitch)
        {
            await this._pitchRepository.AddAsync(pitch);
            await this._pitchRepository.SaveChangesAsync();
        }

        public TViewModel GetPitch<TViewModel>(string id) => this._pitchRepository
                .All()
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();

        public async Task<Pitch> GetPitchAsync(string id) => await this._pitchRepository.GetByIdAsync(id);

        public IEnumerable<TViewModel> GetPitches<TViewModel>() => this._pitchRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>();

        public async Task RemovePitchAsync(Pitch pitch)
        {
            this._pitchRepository.Delete(pitch);
            await this._pitchRepository.SaveChangesAsync();
        }

        public async Task UpdatePitchAsync(Pitch pitch)
        {
            this._pitchRepository.Update(pitch);
            await this._pitchRepository.SaveChangesAsync();
        }
    }
}