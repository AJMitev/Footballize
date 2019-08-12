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
        private readonly IDeletableEntityRepository<Province> provincesRepository;

        public ProvinceService(IDeletableEntityRepository<Province> provincesRepository)
        {
            this.provincesRepository = provincesRepository;
        }

        public IEnumerable<TViewModel> GetProvinces<TViewModel>()
        {
            return this.provincesRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>();
        }

        public IEnumerable<TViewModel> GetProvincesByCountry<TViewModel>(string countryId)
        {
            return this.provincesRepository
                .All()
                .Where(p => p.CountryId.Equals(countryId))
                .OrderBy(p=>p.Name)
                .ThenByDescending(p=>p.Towns.Count)
                .To<TViewModel>();
        }

        public async Task CreateProvinceAsync(Province province)
        {
            await this.provincesRepository.AddAsync(province);
            await this.provincesRepository.SaveChangesAsync();
        }

        public async Task RemoveProvinceAsync(string id)
        {
            var provinceToRemove = await this.provincesRepository.GetByIdAsync(id);
            this.provincesRepository.Delete(provinceToRemove);
            await this.provincesRepository.SaveChangesAsync();
        }

        public TViewModel GetProvince<TViewModel>(string id)
        {
            return this.provincesRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .SingleOrDefault();
        }

        public async Task UpdateProvinceAsync(Province province)
        {
            this.provincesRepository.Update(province);
            await this.provincesRepository.SaveChangesAsync();
        }
    }
}