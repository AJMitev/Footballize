namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class GatherService : IGatherServices
    {
        private readonly IRepository<Gather> gatherRepository;
        private readonly IRepository<GatherUser> gatherUserRepository;

        public GatherService(IRepository<Gather> gatherRepository, IRepository<GatherUser> gatherUserRepository)
        {
            this.gatherRepository = gatherRepository;
            this.gatherUserRepository = gatherUserRepository;
        }

        public TViewModel GetGather<TViewModel>(string id)
        {
            return this.gatherRepository
                .All()
                .Where(x=>x.Id.Equals(id))
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

        public ICollection<TViewModel> GetGathers<TViewModel>()
        {
            return this.gatherRepository
                .All()
                .To<TViewModel>()
                .ToList();
        }
    }
}