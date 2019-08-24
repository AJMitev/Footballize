namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public class RecruitmentService : IRecruitmentService
    {
        private readonly IDeletableEntityRepository<Recruitment> recruitmentRepository;
        private readonly IDeletableEntityRepository<RecruitmentUser> recruiterUserRepository;
        private readonly IDeletableEntityRepository<User> userRepository;

        public RecruitmentService(IDeletableEntityRepository<Recruitment> recruitmentRepository, IDeletableEntityRepository<RecruitmentUser> recruiterUserRepository, IDeletableEntityRepository<User> userRepository)
        {
            this.recruitmentRepository = recruitmentRepository;
            this.recruiterUserRepository = recruiterUserRepository;
            this.userRepository = userRepository;
        }

        public ICollection<TViewModel> GetRecruitments<TViewModel>()
        {
            return this.recruitmentRepository
                .All()
                .OrderBy(x => x.CreatedOn)
                .To<TViewModel>()
                .ToList();
        }

        public TViewModel GetRecruitment<TViewModel>(string id)
        {
            return this.recruitmentRepository
                .All()
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();
        }

        public Recruitment GetRecruitmentWithPlayers(string id)
        {
            return this.recruitmentRepository
                .All()
                .Include(x => x.RecruitedUsers)
                .SingleOrDefault(x => x.Id == id);
        }

        public async Task AddRecruitmentAsync(Recruitment recruitment)
        {
            if (recruitment == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            await this.recruitmentRepository.AddAsync(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task LeaveRecruitmentAsync(Recruitment recruitment, string userId)
        {
            if (recruitment == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            if (recruitment.Status != GameStatus.Registration)
            {
                throw new ServiceException(ServiceException.KickPlayerOnlyInRegistrationMode);
            }

            var gameUser = recruitment?.RecruitedUsers.SingleOrDefault(u => u.UserId.Equals(userId));

            if (gameUser == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            this.recruiterUserRepository.Delete(gameUser);

            recruitment.RecruitedUsers.Remove(gameUser);
            this.recruitmentRepository.Update(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();
            await this.recruiterUserRepository.SaveChangesAsync();
        }

        public async Task EnrollRecruitmentAsync(Recruitment recruitment, User user)
        {

            if (user == null || recruitment == null)
                throw new ServiceException(ServiceException.InvalidRequestParameters);

            if (recruitment.Status != GameStatus.Registration || recruitment.RecruitedUsers.Count >= recruitment.MaximumPlayers)
            {
                throw new ServiceException(ServiceException.NotInRegistrationOrNoFreeSlot);
            }

            if (recruitment.RecruitedUsers.Any(x => x.UserId == user.Id))
            {
                throw new ServiceException(ServiceException.AlreadyJoined);
            }

            var enrolledGame = new RecruitmentUser
            {
                User = user,
                Recruitment = recruitment
            };

            recruitment.RecruitedUsers.Add(enrolledGame);

            await this.recruiterUserRepository.AddAsync(enrolledGame);
            await this.recruiterUserRepository.SaveChangesAsync();

        }

        public async Task StartRecruitmentAsync(string id)
        {
            var gameToStart = await this.recruitmentRepository.GetByIdAsync(id);

            if (gameToStart == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            if (gameToStart.RecruitedUsers.Count != gameToStart.MaximumPlayers)
            {
                throw new ServiceException(ServiceException.RequiredNumberOfPlayersNotReached);
            }

            if (gameToStart.Status != GameStatus.Registration)
            {
                throw new ServiceException(ServiceException.ThisGameIsAlreadyStarted);
            }

            gameToStart.Status = GameStatus.Started;

            this.recruitmentRepository.Update(gameToStart);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task CompleteRecruitmentAsync(string id)
        {
            var game = await this.recruitmentRepository.GetByIdAsync(id);

            if (game == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            if (game.Status != GameStatus.Started)
            {
                throw new ServiceException(ServiceException.ThisGameIsNotStartedYet);
            }

            game.Status = GameStatus.Finished;

            this.recruitmentRepository.Update(game);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task DeleteRecruitmentAsync(string id)
        {
            var game = await this.recruitmentRepository.GetByIdAsync(id);

            if (game == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            this.recruitmentRepository.Delete(game);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task<Recruitment> GetRecruitmentAsync(string id)
        {
            return await this.recruitmentRepository.GetByIdAsync(id);
        }
    }
}