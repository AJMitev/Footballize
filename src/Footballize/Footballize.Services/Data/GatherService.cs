namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public class GatherService : IGatherServices
    {
        private readonly IDeletableEntityRepository<Gather> gatherRepository;
        private readonly IDeletableEntityRepository<GatherUser> gatherUserRepository;
        private readonly IDeletableEntityRepository<User> userRepository;

        public GatherService(IDeletableEntityRepository<Gather> gatherRepository, IDeletableEntityRepository<GatherUser> gatherUserRepository, IDeletableEntityRepository<User> userRepository)
        {
            this.gatherRepository = gatherRepository;
            this.gatherUserRepository = gatherUserRepository;
            this.userRepository = userRepository;
        }

        public TViewModel GetGather<TViewModel>(string id)
        {
            return this.gatherRepository
                .All()
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();
        }

        public async Task AddGatherAsync(Gather gather)
        {
            if (gather == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            var gatherUser = new GatherUser
            {
                Gather = gather,
                User = gather.Creator
            };

            gather.Players.Add(gatherUser);

            await this.gatherUserRepository.AddAsync(gatherUser);
            await this.gatherRepository.AddAsync(gather);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task LeaveGatherAsync(Gather gather, string userId)
        {
            if (gather == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            if (gather.Status != GameStatus.Registration)
            {
                throw new ServiceException(ServiceException.KickPlayerOnlyInRegistrationMode);
            }

            var gatherUser = gather?.Players.SingleOrDefault(u => u.UserId.Equals(userId));

            if (gatherUser == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            this.gatherUserRepository.Delete(gatherUser);

            gather.Players.Remove(gatherUser);
            this.gatherRepository.Update(gather);

            await this.gatherUserRepository.SaveChangesAsync();
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task EnrollGatherAsync(Gather gather, User user)
        {
            if (user == null || gather == null)
                throw new ServiceException(ServiceException.InvalidRequestParameters);

            if (gather.Status != GameStatus.Registration || gather.Players.Count >= gather.MaximumPlayers)
            {
                throw new ServiceException(ServiceException.NotInRegistrationOrNoFreeSlot);
            }

            if (gather.Players.Any(x => x.UserId == user.Id))
            {
                throw new ServiceException(ServiceException.AlreadyJoined);
            }

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

        public ICollection<TViewModel> GetGathers<TViewModel>()
        {
            return this.gatherRepository
                .All()
                .To<TViewModel>()
                .ToList();
        }

        public async Task StartGatherAsync(string id)
        {
            var gather = await this.gatherRepository.GetByIdAsync(id);

            if (gather == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            if (gather.Players.Count != gather.MaximumPlayers)
            {
                throw new ServiceException(ServiceException.RequiredNumberOfPlayersNotReached);
            }

            if (gather.Status != GameStatus.Registration)
            {
                throw new ServiceException(ServiceException.ThisGameIsAlreadyStarted);
            }

            gather.Status = GameStatus.Started;

            this.gatherRepository.Update(gather);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task CompleteGatherAsync(string id)
        {
            var game = await this.gatherRepository.GetByIdAsync(id);

            if (game == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            if (game.Status != GameStatus.Started)
            {
                throw new ServiceException(ServiceException.ThisGameIsNotStartedYet);
            }

            game.Status = GameStatus.Finished;

            this.gatherRepository.Update(game);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task DeleteGatherAsync(string id)
        {
            var gatherToDelete = await this.gatherRepository.GetByIdAsync(id);

            if (gatherToDelete == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            this.gatherRepository.Delete(gatherToDelete);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task<Gather> GetGatherAsync(string id)
        {
            return await this.gatherRepository.GetByIdAsync(id);
        }

        public Gather GetGatherWithPlayers(string id)
        {
            return this.gatherRepository.All().Include(x => x.Players).SingleOrDefault(x => x.Id == id);
        }
    }
}