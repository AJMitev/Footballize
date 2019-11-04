namespace Footballize.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Recruitment;

    public class RecruitmentService : IRecruitmentService
    {
        private readonly IDeletableEntityRepository<Recruitment> recruitmentRepository;
        private readonly IDeletableEntityRepository<RecruitmentUser> recruiterUserRepository;
        private readonly IDeletableEntityRepository<User> userRepository;

        public RecruitmentService(IDeletableEntityRepository<Recruitment> recruitmentRepository,
            IDeletableEntityRepository<RecruitmentUser> recruiterUserRepository,
            IDeletableEntityRepository<User> userRepository)
        {
            this.recruitmentRepository = recruitmentRepository;
            this.recruiterUserRepository = recruiterUserRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<TViewModel> GetAll<TViewModel>()
            => this.recruitmentRepository
                .All()
                .Where(x => x.StartingAt > DateTime.UtcNow)
                .OrderBy(x => x.StartingAt)
                .ThenBy(x => x.Status)
                .To<TViewModel>()
                .ToList();

        public IEnumerable<TViewModel> GetAll<TViewModel>(Expression<Func<Recruitment, bool>> expression)
            => this.recruitmentRepository
                .All()
                .Where(x => x.StartingAt > DateTime.UtcNow)
                .Where(expression)
                .OrderBy(x => x.StartingAt)
                .ThenBy(x => x.Status)
                .To<TViewModel>()
                .ToList();

        public TViewModel GetById<TViewModel>(string id)
            => this.recruitmentRepository
                .All()
                .Include(x => x.Players)
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();

        public async Task<RecruitmentServiceModel> GetByIdAsync(string id)
            => await this.recruitmentRepository
                .All()
                .Where(x=>x.Id == id)
                .To<RecruitmentServiceModel>()
                .SingleOrDefaultAsync();

        public async Task<string> AddAsync(string title, DateTime startingAt, string pitchId, string creatorId, int maximumPlayers)
        {
            var recruitment = new Recruitment
            {
                Title = title,
                StartingAt = startingAt,
                PitchId = pitchId,
                CreatorId = creatorId,
                MaximumPlayers = maximumPlayers,
                Status = GameStatus.Registration
            };

            await this.recruitmentRepository.AddAsync(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();

            return recruitment.Id;
        }

        public async Task LeaveAsync(string gameId, string userId)
        {
            var recruitment = await this.recruitmentRepository.All()
                .Where(x=>x.Id == gameId)
                .Include(x=>x.Players)
                .SingleOrDefaultAsync();
            
            var gameUser = recruitment?.Players.SingleOrDefault(u => u.UserId.Equals(userId));

            this.recruiterUserRepository.Delete(gameUser);

            recruitment.Players.Remove(gameUser);
            this.recruitmentRepository.Update(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();
            await this.recruiterUserRepository.SaveChangesAsync();
        }

        public async Task EnrollAsync(string gameId, string userId)
        {
            var recruitment =  await this.recruitmentRepository.GetByIdAsync(gameId);
            var user = await this.userRepository.GetByIdAsync(userId);
            
            var enrolledGame = new RecruitmentUser
            {
                User = user,
                Recruitment = recruitment
            };

            recruitment.Players.Add(enrolledGame);

            await this.recruiterUserRepository.AddAsync(enrolledGame);
            await this.recruiterUserRepository.SaveChangesAsync();
        }

        public async Task StartAsync(string id)
        {
            var gameToStart = this.recruitmentRepository.All()
                .Include(x => x.Players)
                .SingleOrDefault(x => x.Id == id);

            gameToStart.Status = GameStatus.Started;

            this.recruitmentRepository.Update(gameToStart);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task CompleteAsync(string id)
        {
            var game = await this.recruitmentRepository.GetByIdAsync(id);
            game.Status = GameStatus.Finished;

            this.recruitmentRepository.Update(game);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var game = await this.recruitmentRepository.GetByIdAsync(id);
            this.recruitmentRepository.Delete(game);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public bool Exists(string id)
            => this.recruitmentRepository
                .All()
                .Any(x => x.Id == id);

        public int GetCount()
            => this.recruitmentRepository
                .All()
                .Count();
    }
}