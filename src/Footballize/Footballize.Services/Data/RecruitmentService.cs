namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class RecruitmentService : IRecruitmentService
    {
        private readonly IDeletableEntityRepository<Recruitment> recruitmentRepository;
        private readonly IDeletableEntityRepository<RecruitmentUser> recruiterUserRepository;

        public RecruitmentService(IDeletableEntityRepository<Recruitment> recruitmentRepository, IDeletableEntityRepository<RecruitmentUser> recruiterUserRepository)
        {
            this.recruitmentRepository = recruitmentRepository;
            this.recruiterUserRepository = recruiterUserRepository;
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
            var recruitingUser = new RecruitmentUser
            {
                User = recruitment.Creator,
                Recruitment = recruitment
            };

            await this.recruiterUserRepository.AddAsync(recruitingUser);
            await this.recruitmentRepository.AddAsync(recruitment);
            await this.recruitmentRepository.SaveChangesAsync();
        }

        public Task LeaveRecruitmentAsync(string recruitmentId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task EnrollRecruitmentAsync(string recruitmentId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task StartRecruitment(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task CompleteRecruitment(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteRecruitment(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}