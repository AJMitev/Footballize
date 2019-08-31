namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using DTOs;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PitchService : IPitchService
    {
        private readonly IDeletableEntityRepository<Pitch> pitchRepository;
        private readonly IRepository<Gather> gatherRepository;
        private readonly IRepository<Recruitment> recruitmentRepository;

        public PitchService(IDeletableEntityRepository<Pitch> pitchRepository, IRepository<Gather> gatherRepository, IRepository<Recruitment> recruitmentRepository)
        {
            this.pitchRepository = pitchRepository;
            this.gatherRepository = gatherRepository;
            this.recruitmentRepository = recruitmentRepository;
        }

        public async Task AddPitchAsync(Pitch pitch)
        {
            if (pitch == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Pitch)));
            }

            await this.pitchRepository.AddAsync(pitch);
            await this.pitchRepository.SaveChangesAsync();
        }

        public IEnumerable<MostUsedPitchDTO> GetMostUsedPitches(int count = 3)
        {

            var mostUsedInGather = this.gatherRepository.All().GroupBy(x => x.Pitch.Id,
                x => x.Pitch.Name,
                (k, g) => new MostUsedPitchDTO { Id = k, Name = g.FirstOrDefault(), TimesUsed = g.Count() });


            var mostUsedInRecruiting = this.recruitmentRepository.All().GroupBy(x => x.Pitch.Id,
                x => x.Pitch.Name,
                (k, g) => new MostUsedPitchDTO { Id = k, Name = g.FirstOrDefault(), TimesUsed = g.Count() });

            var mostUsedPitches = new HashSet<MostUsedPitchDTO>(mostUsedInGather);

            foreach (var pitch in mostUsedInRecruiting)
            {
                var fieldFromDb = this.pitchRepository.All()
                    .Include(x => x.Address)
                    .ThenInclude(x => x.Town)
                    .ThenInclude(x => x.Province)
                    .ThenInclude(x => x.Country)
                    .SingleOrDefault(x => x.Id == pitch.Id);

                if (fieldFromDb != null)
                {
                    var fieldLocation = string.Concat(fieldFromDb.Address.Town.Name, ", ", fieldFromDb.Address.Town.Province.Country.Name);

                    pitch.Location = fieldLocation;
                }

                if (mostUsedPitches.Any(x => x.Id == pitch.Id))
                {
                    var current = mostUsedPitches.SingleOrDefault(x => x.Id == pitch.Id);
                    current.TimesUsed += pitch.TimesUsed;
                    current.Location = pitch.Location;

                    continue;
                }

               

                mostUsedPitches.Add(pitch);
            }


            return mostUsedPitches.Take(count);
        }

        public TViewModel GetPitch<TViewModel>(string id) => this.pitchRepository
                .All()
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();

        public async Task<Pitch> GetPitchAsync(string id) => await this.pitchRepository.GetByIdAsync(id);

        public IEnumerable<TViewModel> GetPitches<TViewModel>() => this.pitchRepository
                .All()
                .OrderBy(x => x.Name)
                .Include(x=>x.Address)
                .ThenInclude(x=>x.Location)
                .Include(x=>x.Address)
                .ThenInclude(x=>x.Town)
                .ThenInclude(x=>x.Province)
                .ThenInclude(x=>x.Country)
                .To<TViewModel>()
                .ToList();

        public IEnumerable<TViewModel> GetPitchesByTownId<TViewModel>(string id)
        {
            return this.pitchRepository
                 .All()
                 .Where(x => x.Address.TownId.Equals(id))
                 .OrderBy(x => x.Name)
                 .To<TViewModel>();
        }

        public async Task RemovePitchAsync(Pitch pitch)
        {
            if (pitch == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Pitch)));
            }

            this.pitchRepository.Delete(pitch);
            await this.pitchRepository.SaveChangesAsync();
        }

        public async Task UpdatePitchAsync(Pitch pitch)
        {
            if (pitch == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Pitch)));
            }

            this.pitchRepository.Update(pitch);
            await this.pitchRepository.SaveChangesAsync();
        }
    }
}