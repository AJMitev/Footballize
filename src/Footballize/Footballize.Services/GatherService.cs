namespace Footballize.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Data.Repositories;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Gather;

    public class GatherService : IGatherService
    {
        private readonly IDeletableEntityRepository<Gather> gatherRepository;
        private readonly IDeletableEntityRepository<GatherUser> gatherUserRepository;
        private readonly IDeletableEntityRepository<User> userRepository;

        public GatherService(IDeletableEntityRepository<Gather> gatherRepository,
            IDeletableEntityRepository<GatherUser> gatherUserRepository,
            IDeletableEntityRepository<User> userRepository)
        {
            this.gatherRepository = gatherRepository;
            this.gatherUserRepository = gatherUserRepository;
            this.userRepository = userRepository;
        }

        public async Task<string> AddAsync(string title, string description, DateTime startingAt, TeamFormat teamFormat, string pitchId, string creatorId)
        {
            var maximumPlayers = CalculateMaximumPlayers(teamFormat);

            var gather = new Gather
            {
                Title = title,
                Description = description,
                StartingAt = startingAt,
                TeamFormat = teamFormat,
                PitchId = pitchId,
                CreatorId = creatorId,
                Status = GameStatus.Registration,
                MaximumPlayers = maximumPlayers
            };

            await this.gatherRepository.AddAsync(gather);
            await this.gatherRepository.SaveChangesAsync();

            return gather.Id;
        }


        public async Task LeaveAsync(string gatherId, string userId)
        {
            var gather = this.gatherRepository.All().Include(x => x.Players).SingleOrDefault(x => x.Id == gatherId);
            var gatherUser = gather?.Players.SingleOrDefault(u => u.UserId.Equals(userId));

            this.gatherUserRepository.Delete(gatherUser);

            if (gather != null)
            {
                gather.Players.Remove(gatherUser);
                this.gatherRepository.Update(gather);
            }

            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task EnrollAsync(string gatherId, string userId)
        {
            var gather = await this.gatherRepository.GetByIdAsync(gatherId);
            var user = await this.userRepository.GetByIdAsync(userId);

            var gatherUser = new GatherUser
            {
                Gather = gather,
                User = user
            };

            await this.gatherUserRepository.AddAsync(gatherUser);
            gather.Players.Add(gatherUser);
            this.gatherRepository.Update(gather);

            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task StartAsync(string id)
        {
            var gather = await this.gatherRepository.GetByIdAsync(id);

            gather.Status = GameStatus.Started;

            this.gatherRepository.Update(gather);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task CompleteAsync(string id)
        {
            var game = await this.gatherRepository.GetByIdAsync(id);

            game.Status = GameStatus.Finished;

            this.gatherRepository.Update(game);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var gatherToDelete = await this.gatherRepository.GetByIdAsync(id);

            this.gatherRepository.Delete(gatherToDelete);
            await this.gatherRepository.SaveChangesAsync();
        }

        public int GetCount()
            => this.gatherRepository
                .All()
                .Count();

        public TViewModel GetById<TViewModel>(string id)
           => this.gatherRepository
               .All()
               .Where(x => x.Id.Equals(id))
               .To<TViewModel>()
               .SingleOrDefault();

        public async Task<GatherServiceModel> GetByIdAsync(string id)
            => await this.gatherRepository
                .All()
                .Where(x => x.Id == id)
                .To<GatherServiceModel>()
                .SingleAsync();


        IEnumerable<TViewModel> IGatherService.GetAll<TViewModel>()
             => this.gatherRepository
                 .All()
                 .To<TViewModel>()
                 .ToList();

        IEnumerable<TViewModel> IGatherService.GetAll<TViewModel>(Expression<Func<Gather, bool>> expression)
            => this.gatherRepository
                .All()
                .Where(x => x.StartingAt > DateTime.UtcNow)
                .Where(expression)
                .OrderBy(x => x.StartingAt)
                .ThenBy(x => x.Status)
                .To<TViewModel>()
                .ToList();

        private static int CalculateMaximumPlayers(TeamFormat teamFormat)
        {
            switch (teamFormat)
            {
                case TeamFormat.FourPlusOne:
                    return 10;
                case TeamFormat.FivePlusOne:
                    return 12;
                case TeamFormat.SixPlusOne:
                    return 14;
                case TeamFormat.ElevenPlayers:
                    return 22;
                default:
                    return -1;
            }
        }
    }
}