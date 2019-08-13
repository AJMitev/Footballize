namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task AddRecruitmentAsync(Recruitment recruitment)
        {
            await this.recruitmentRepository.AddAsync(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task LeaveRecruitmentAsync(string recruitmentId, string userId)
        {
            var game = this.recruitmentRepository
                .All()
                .Include(x => x.RecruitedUsers)
                .SingleOrDefault(x => x.Id == recruitmentId);

            var gameUser = game?.RecruitedUsers.SingleOrDefault(user => user.UserId.Equals(userId));

            if (gameUser == null)
            {
                return;
            }

            this.recruiterUserRepository.Delete(gameUser);

            game.RecruitedUsers.Remove(gameUser);
            this.recruitmentRepository.Update(game);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task EnrollRecruitmentAsync(string recruitmentId, string userId)
        {
            var gameToEnroll = await this.recruitmentRepository.GetByIdAsync(recruitmentId);
            var enrolledUser = await this.userRepository.GetByIdAsync(userId);

            if (gameToEnroll == null || enrolledUser == null)
            {
                return;
            }

            var enrolledGame = new RecruitmentUser
            {
                User = enrolledUser,
                Recruitment = gameToEnroll
            };

            gameToEnroll.RecruitedUsers.Add(enrolledGame);

            await this.recruiterUserRepository.AddAsync(enrolledGame);
            await this.recruiterUserRepository.SaveChangesAsync();

        }

        public async Task StartRecruitmentAsync(string id)
        {
            var gameToStart = await this.recruitmentRepository.GetByIdAsync(id);
            gameToStart.Status = GameStatus.Started;

            this.recruitmentRepository.Update(gameToStart);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task CompleteRecruitmentAsync(string id)
        {
            var gameToStart = await this.recruitmentRepository.GetByIdAsync(id);
            gameToStart.Status = GameStatus.Finished;

            this.recruitmentRepository.Update(gameToStart);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task DeleteRecruitmentAsync(string id)
        {
            var game = await this.recruitmentRepository.GetByIdAsync(id);
            this.recruitmentRepository.Delete(game);
            await this.recruitmentRepository.SaveChangesAsync();
        }
    }
}