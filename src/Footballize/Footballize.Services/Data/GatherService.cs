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

            if (gather == null || gather.Status != GameStatus.Registration)
                return;

            var gatherUser = gather?.Players.SingleOrDefault(u => u.UserId.Equals(userId));

            if (gatherUser == null)
            {
                return;
            }


            this.gatherUserRepository.Delete(gatherUser);

            gather.Players.Remove(gatherUser);
            this.gatherRepository.Update(gather);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task EnrollGatherAsync(Gather gather, User user)
        {
            if (user == null || gather == null || gather.Status != GameStatus.Registration || gather.Players.Count >= gather.MaximumPlayers)
                return;

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

        public async Task StartGather(string id)
        {
            var gather = await this.gatherRepository.GetByIdAsync(id);

            if (gather == null)
            {
                return;
            }

            gather.Status = GameStatus.Started;

            this.gatherRepository.Update(gather);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task CompleteGather(string id)
        {
            var gather = await this.gatherRepository.GetByIdAsync(id);

            if (gather == null)
            {
                return;
            }

            gather.Status = GameStatus.Finished;

            this.gatherRepository.Update(gather);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task DeleteGatherAsync(string id)
        {
            var gatherToDelete = await this.gatherRepository.GetByIdAsync(id);

            if (gatherToDelete == null)
            {
                return;
            }

            this.gatherRepository.Delete(gatherToDelete);
            await this.gatherRepository.SaveChangesAsync();
        }

        public async Task<Gather> GetGatherAsync(string id)
        {
            return await this.gatherRepository.GetByIdAsync(id);
        }
    }
}