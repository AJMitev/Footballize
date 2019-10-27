namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Footballize.Services.Mapping;
    using Footballize.Services.Models.Gather;
    using Microsoft.EntityFrameworkCore;
    using GlobalConstants = Common.GlobalConstants;

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
        
        public async Task<string> AddAsync(string title, string description, DateTime startingAt, TeamFormat teamFormat, string pitchId)
        {
            var gather = new Gather
            {
                Title = title,
                Description = description,
                StartingAt = startingAt,
                TeamFormat = teamFormat,
                PitchId = pitchId
            };

            await this.gatherRepository.AddAsync(gather);
            await this.gatherRepository.SaveChangesAsync();

            return gather.Id;
        }

        public async Task LeaveAsync(string gatherId, string userId)
        {
            var gather = await this.gatherRepository.GetByIdAsync(gatherId);

            if (gather == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Gather)));
            }

            if (gather.Status != GameStatus.Registration)
            {
                throw new ServiceException(GlobalConstants.KickPlayerOnlyInRegistrationModeErrorMessage);
            }

            var gatherUser = gather?.Players.SingleOrDefault(u => u.UserId.Equals(userId));

            if (gatherUser == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            this.gatherUserRepository.Delete(gatherUser);

            gather.Players.Remove(gatherUser);
            this.gatherRepository.Update(gather);

            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task EnrollAsync(string gatherId, string userId)
        {
            var gather = await this.gatherRepository.GetByIdAsync(gatherId);
            var user = await this.userRepository.GetByIdAsync(userId);


            if (gather == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Gather)));
            }

            if (user == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(User)));

            }

            if (gather.Status != GameStatus.Registration || gather.Players.Count >= gather.MaximumPlayers)
            {
                throw new ServiceException(Common.GlobalConstants.NotInRegistrationOrNoFreeSlotErrorMessage);
            }

            if (user.IsBanned)
            {
                throw new ServiceException(Common.GlobalConstants.PlayerIsBannedErrorMessage);
            }

            if (gather.Players.Any(x => x.UserId == user.Id))
            {
                throw new ServiceException(Common.GlobalConstants.AlreadyJoinedErrorMessage);
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



        public async Task StartAsync(string id)
        {
            var gather = await this.gatherRepository.GetByIdAsync(id);

            if (gather == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Gather)));
            }

            if (gather.Players.Count != gather.MaximumPlayers)
            {
                throw new ServiceException(Common.GlobalConstants.RequiredNumberOfPlayersNotReachedErrorMessage);
            }

            if (gather.Status != GameStatus.Registration)
            {
                throw new ServiceException(Common.GlobalConstants.ThisGameIsAlreadyStartedErrorMessage);
            }

            gather.Status = GameStatus.Started;

            this.gatherRepository.Update(gather);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task CompleteAsync(string id)
        {
            var game = await this.gatherRepository.GetByIdAsync(id);

            if (game == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Gather)));
            }

            if (game.Status != GameStatus.Started)
            {
                throw new ServiceException(Common.GlobalConstants.ThisGameIsNotStartedYetErrorMessage);
            }

            game.Status = GameStatus.Finished;

            this.gatherRepository.Update(game);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var gatherToDelete = await this.gatherRepository.GetByIdAsync(id);

            if (gatherToDelete == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Gather)));
            }

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
    }
}