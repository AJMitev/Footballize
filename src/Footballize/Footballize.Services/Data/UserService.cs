namespace Footballize.Services.Data
{
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

        public UserService(IDeletableEntityRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddPlaypal(User playpal, User currentUser)
        {
            if (playpal == null || currentUser == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            if (playpal.Id == currentUser.Id)
            {
                throw new ServiceException(InvalidPlaypal);
            }

            if (playpal.Playpals.Contains(currentUser) && currentUser.Playpals.Contains(playpal))
            {
                return;
            }

            currentUser.Playpals.Add(playpal);
            playpal.Playpals.Add(currentUser);

            this.userRepository.Update(currentUser);
            this.userRepository.Update(playpal);
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
            return  this.userRepository
                .All()
                .Include(c => c.GathersPlayed)
                .ThenInclude(x => x.Gather)
                .Include(x => x.GamesRecruited)
                .ThenInclude(x => x.Recruitment)
                .Include(x => x.Playpals)
                .SingleOrDefault(u => u.Id == id);
        }

        public async Task RemovePlaypal(User playpal, User currentUser)
        {
            if (playpal == null || currentUser == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            if (playpal.Id == currentUser.Id)
            {
                throw new ServiceException(InvalidPlaypal);
            }

            if (!playpal.Playpals.Contains(currentUser) && !currentUser.Playpals.Contains(playpal))
            {
                return;
            }

            currentUser.Playpals.Remove(playpal);
            playpal.Playpals.Remove(currentUser);

            this.userRepository.Update(playpal);
            this.userRepository.Update(currentUser);

            await this.userRepository.SaveChangesAsync();
        }
    }
}