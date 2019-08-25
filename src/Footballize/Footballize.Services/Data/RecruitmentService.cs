namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
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
                .Include(x => x.Players)
                .SingleOrDefault(x => x.Id == id);
        }

        public async Task AddRecruitmentAsync(Recruitment recruitment)
        {
            if (recruitment == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            await this.recruitmentRepository.AddAsync(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task LeaveRecruitmentAsync(Recruitment recruitment, string userId)
        {
            if (recruitment == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            if (recruitment.Status != GameStatus.Registration)
            {
                throw new ServiceException(Common.GlobalConstants.KickPlayerOnlyInRegistrationModeErrorMessage);
            }

            var gameUser = recruitment?.Players.SingleOrDefault(u => u.UserId.Equals(userId));

            if (gameUser == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            this.recruiterUserRepository.Delete(gameUser);

            recruitment.Players.Remove(gameUser);
            this.recruitmentRepository.Update(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();
            await this.recruiterUserRepository.SaveChangesAsync();
        }

        public async Task EnrollRecruitmentAsync(Recruitment recruitment, User user)
        {

            if (user == null || recruitment == null)
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);

            if (recruitment.Status != GameStatus.Registration || recruitment.Players.Count >= recruitment.MaximumPlayers)
            {
                throw new ServiceException(Common.GlobalConstants.NotInRegistrationOrNoFreeSlotErrorMessage);
            }

            if (user.IsBanned)
            {
                throw new ServiceException(Common.GlobalConstants.PlayerIsBannedErrorMessage);
            }

            if (recruitment.Players.Any(x => x.UserId == user.Id))
            {
                throw new ServiceException(Common.GlobalConstants.AlreadyJoinedErrorMessage);
            }

            var enrolledGame = new RecruitmentUser
            {
                User = user,
                Recruitment = recruitment
            };

            recruitment.Players.Add(enrolledGame);

            await this.recruiterUserRepository.AddAsync(enrolledGame);
            await this.recruiterUserRepository.SaveChangesAsync();

        }

        public async Task StartRecruitmentAsync(string id)
        {
            var gameToStart = await this.recruitmentRepository.GetByIdAsync(id);

            if (gameToStart == null)
            {
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            if (gameToStart.Players.Count != gameToStart.MaximumPlayers)
            {
                throw new ServiceException(Common.GlobalConstants.RequiredNumberOfPlayersNotReachedErrorMessage);
            }

            if (gameToStart.Status != GameStatus.Registration)
            {
                throw new ServiceException(Common.GlobalConstants.ThisGameIsAlreadyStartedErrorMessage);
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
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
            }

            if (game.Status != GameStatus.Started)
            {
                throw new ServiceException(Common.GlobalConstants.ThisGameIsNotStartedYetErrorMessage);
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
                throw new ServiceException(GlobalConstants.InvalidRequestParametersErrorMessage);
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