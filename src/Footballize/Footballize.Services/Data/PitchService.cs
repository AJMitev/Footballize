﻿namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Pitch;

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

        public TViewModel GetById<TViewModel>(string id)
            => this.pitchRepository
                .All()
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();

        public IEnumerable<TViewModel> GetAll<TViewModel>()
            => this.pitchRepository
                .All()
                .OrderBy(x => x.Name)
                .Include(x => x.Address)
                .ThenInclude(x => x.Location)
                .Include(x => x.Address)
                .ThenInclude(x => x.Town)
                .ThenInclude(x => x.Province)
                .ThenInclude(x => x.Country)
                .To<TViewModel>()
                .ToList();

        public IEnumerable<TViewModel> GetByTownId<TViewModel>(string id)
            => this.pitchRepository
                 .All()
                 .Where(x => x.Address.TownId.Equals(id))
                 .OrderBy(x => x.Name)
                 .To<TViewModel>();

        public async Task<PitchServiceModel> GetByIdAsync(string id)
            => await this.pitchRepository
                .All()
                .Where(x => x.Id == id)
                .To<PitchServiceModel>()
                .SingleAsync();

        public async Task AddAsync(string name, string addressId)
        {
            var pitch = new Pitch
            {
                Name = name,
                AddressId = addressId
            };

            await this.pitchRepository.AddAsync(pitch);
            await this.pitchRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, string name, string addressId)
        {
            var pitch = await this.pitchRepository.GetByIdAsync(id);

            pitch.Name = name;
            pitch.AddressId = addressId;

            this.pitchRepository.Update(pitch);
            await this.pitchRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var pitch = await this.pitchRepository.GetByIdAsync(id);

            // TODO: Should I throw exception into the services?
            if (pitch == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Pitch)));
            }

            this.pitchRepository.Delete(pitch);
            await this.pitchRepository.SaveChangesAsync();
        }
        public bool Exist(string name, string addressId)
            => this.pitchRepository
                .All()
                .Any(x => x.Name == name && x.AddressId == addressId);

        public IEnumerable<MostUsedPitchServiceModel> GetMostUsed(int count = 3)
        {
            var mostUsedInGather = GetMostUsedPitchesInGathers().ToList();
            var mostUsedInRecruiting = GetMostUsedPitchesInRecruitingGames().ToList();
            HashSet<MostUsedPitchServiceModel> mostUsedPitches = GetMostUsedPitchFromAllGames(mostUsedInGather, mostUsedInRecruiting);

            return mostUsedPitches.Take(count);
        }

        private HashSet<MostUsedPitchServiceModel> GetMostUsedPitchFromAllGames(ICollection<MostUsedPitchServiceModel> mostUsedInGather, ICollection<MostUsedPitchServiceModel> mostUsedInRecruiting)
        {
            var mostUsedPitches = new HashSet<MostUsedPitchServiceModel>(mostUsedInGather);

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

            return mostUsedPitches;
        }

        private IEnumerable<MostUsedPitchServiceModel> GetMostUsedPitchesInRecruitingGames()
        {
            return this.recruitmentRepository.All().GroupBy(x => x.Pitch.Id,
                x => x.Pitch.Name,
                (k, g) => new MostUsedPitchServiceModel { Id = k, Name = g.FirstOrDefault(), TimesUsed = g.Count() }).ToList();
        }

        private IEnumerable<MostUsedPitchServiceModel> GetMostUsedPitchesInGathers()
        {
            return this.gatherRepository.All().GroupBy(x => x.Pitch.Id,
                x => x.Pitch.Name,
                (k, g) => new MostUsedPitchServiceModel { Id = k, Name = g.FirstOrDefault(), TimesUsed = g.Count() }).ToList();
        }
    }
}