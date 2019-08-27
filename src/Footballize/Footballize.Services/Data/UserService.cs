namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class UserService : IUserService
    {
        public const string InvalidPlaypal = "This player cannot be part of your playpals list!";

        private readonly IDeletableEntityRepository<User> userRepository;
        private readonly IRepository<Playpal> playpalsRepository;
        private readonly IDeletableEntityRepository<UserReport> reportsRepository;

        public UserService(IDeletableEntityRepository<User> userRepository, IRepository<Playpal> playpalsRepository, IDeletableEntityRepository<UserReport> reportsRepository)
        {
            this.userRepository = userRepository;
            this.playpalsRepository = playpalsRepository;
            this.reportsRepository = reportsRepository;
        }

        public async Task AddPlaypal(User userToAdd, User currentUser)
        {
            if (userToAdd == null || currentUser == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            if (userToAdd.Id == currentUser.Id)
            {
                throw new ServiceException(InvalidPlaypal);
            }


            var newFriendship = new Playpal
            {
                FromUser = currentUser,
                ToUser = userToAdd
            };

            currentUser.PlaypalsAdded.Add(newFriendship);
            userToAdd.PlaypalsAddedMe.Add(newFriendship);

            this.userRepository.Update(currentUser);
            this.userRepository.Update(userToAdd);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string id)
        {
            return await this.userRepository.GetByIdAsync(id);
        }

        public ICollection<TViewModel> GetUsers<TViewModel>()
        {
            return this.userRepository
                .All()
                .To<TViewModel>()
                .ToList();
        }

        public User GetUser(string id)
        {
            return this.userRepository
                .All()
                .Include(c => c.GathersPlayed)
                .ThenInclude(x => x.Gather)
                .Include(x => x.GamesRecruited)
                .ThenInclude(x => x.Recruitment)
                .Include(x => x.PlaypalsAddedMe)
                .ThenInclude(x=>x.FromUser)
                .Include(x => x.PlaypalsAdded)
                .ThenInclude(x=>x.ToUser)
                .SingleOrDefault(u => u.Id == id);
        }

        public async Task RemovePlaypal(string playpalId, string currentUserId)
        {
            if (playpalId == null || currentUserId == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            if (playpalId == currentUserId)
            {
                throw new ServiceException(InvalidPlaypal);
            }

            var friendshipToRemove = this.playpalsRepository.All()
                .SingleOrDefault(x => x.FromUserId == currentUserId && x.ToUserId == playpalId);


            if (friendshipToRemove == null)
            {
                return;
            }

            this.playpalsRepository.Delete(friendshipToRemove);
            await this.playpalsRepository.SaveChangesAsync();
        }

        public async Task BanPlayer(User player, int minutes)
        {
            if (player == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }


            player.IsBanned = true;
            player.BanUntil = DateTime.UtcNow.AddMinutes(minutes);

            this.userRepository.Update(player);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task RemoveBan(User player)
        {
            if (player == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }


            player.IsBanned = false;
            player.BanUntil = null;

            this.userRepository.Update(player);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task CreateReport(UserReport report)
        {
            if (report == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            await this.reportsRepository.AddAsync(report);
            await this.reportsRepository.SaveChangesAsync();
        }

        public ICollection<TViewModel> GetBannedUsers<TViewModel>()
        {
            return this.userRepository
                .All()
                .Where(x => x.IsBanned)
                .To<TViewModel>()
                .ToList();

        }

        public int GetUsersCount()
        {
            return this.userRepository.All().Count();
        }

        public ICollection<TViewModel> GetReportedUsers<TViewModel>()
        {
            return this.reportsRepository
                .All()
                .To<TViewModel>()
                .ToList();
        }
    }
}