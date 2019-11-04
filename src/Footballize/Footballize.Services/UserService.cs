namespace Footballize.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Common;
    using Data.Repositories;
    using Exceptions;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.User;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<User> userRepository;
        private readonly IRepository<Playpal> playpalsRepository;
        private readonly IDeletableEntityRepository<UserReport> reportsRepository;

        public UserService(IDeletableEntityRepository<User> userRepository, IRepository<Playpal> playpalsRepository, IDeletableEntityRepository<UserReport> reportsRepository)
        {
            this.userRepository = userRepository;
            this.playpalsRepository = playpalsRepository;
            this.reportsRepository = reportsRepository;
        }
        
        public IEnumerable<TViewModel> GetAll<TViewModel>()
            => this.userRepository
                .All()
                .To<TViewModel>()
                .ToList();

        public async Task AddPlaypal(string playpalId, string currentUserId)
        {
            var userToAdd = await this.userRepository.GetByIdAsync(playpalId);
            var currentUser = await this.userRepository.GetByIdAsync(currentUserId);
            
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


        public async Task RemovePlaypal(string playpalId, string currentUserId)
        {
          var friendshipToRemove = this.playpalsRepository.All()
                .SingleOrDefault(x => x.FromUserId == currentUserId && x.ToUserId == playpalId);


            if (friendshipToRemove == null)
            {
                return;
            }

            this.playpalsRepository.Delete(friendshipToRemove);
            await this.playpalsRepository.SaveChangesAsync();
        }
        
        public async Task BanPlayer(string userId, int minutes)
        {
            var player = await this.userRepository.GetByIdAsync(userId);

            if (player == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(User)));
            }


            player.IsBanned = true;
            player.BanUntil = DateTime.UtcNow.AddMinutes(minutes);

            this.userRepository.Update(player);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task RemoveBan(string userId)
        {
            var player = await this.userRepository.GetByIdAsync(userId);

            if (player == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(User)));
            }

            player.IsBanned = false;
            player.BanUntil = null;

            this.userRepository.Update(player);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task CreateReport(string text, ReportType type, string reportedUserId)
        {
           var report = new UserReport
           {
               Text = text,
               Type = type,
               ReportedUserId = reportedUserId
           };

            await this.reportsRepository.AddAsync(report);
            await this.reportsRepository.SaveChangesAsync();
        }

        public int GetUsersCount()
            => this.userRepository
                .All()
                .Count();

        public IEnumerable<TViewModel> GetUserReports<TViewModel>()
            => this.reportsRepository
                .All()
                .To<TViewModel>()
                .ToList();

        public bool Exists(string id)
            => this.userRepository
                .All()
                .Any(x => x.Id == id);

        public IEnumerable<TViewModel> GetInactiveUsers<TViewModel>(int days)
            => this.userRepository
                .All()
                .ToList()
                .Where(x => x.GathersPlayed.Any(y => (DateTime.UtcNow - y.CreatedOn).TotalDays >= days)
                            && x.GamesRecruited.Any(y => (DateTime.UtcNow - y.CreatedOn).TotalDays >= days))
            .AsQueryable()
                .To<TViewModel>();
                         

        public IEnumerable<TViewModel> GetAll<TViewModel>(Expression<Func<User, bool>> expression)
            => this.userRepository
                .All()
                .Where(expression)
                .To<TViewModel>()
                .ToList();

        public async Task<TViewModel> GetByIdAsync<TViewModel>(string id)
            => await this.userRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .SingleOrDefaultAsync();

        public UserServiceModel GetById(string id)
            =>  this.GetByIdAsync<UserServiceModel>(id)
                .GetAwaiter()
                .GetResult();
    }
}