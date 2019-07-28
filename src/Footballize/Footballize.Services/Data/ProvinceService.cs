namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class ProvinceService : IProvinceServices
    {
        private readonly IRepository<Province> provincesRepository;

        public ProvinceService(IRepository<Province> provincesRepository)
        {
            this.provincesRepository = provincesRepository;
        }

        public IEnumerable<TViewModel> GetProvinces<TViewModel>()
        {
            return this.provincesRepository
                .All()
                .OrderBy(x=>x.Name)
                .To<TViewModel>();
        }

        public async Task CreateProvince(Province province)
        {
            await this.provincesRepository.AddAsync(province);
            await this.provincesRepository.SaveChangesAsync();
        }
    }
}