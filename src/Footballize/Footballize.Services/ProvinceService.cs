namespace Footballize.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Footballize.Models;
    using Mapping;

    public class ProvinceService : IProvinceService
    {
        private readonly IDeletableEntityRepository<Province> provincesRepository;

        public ProvinceService(IDeletableEntityRepository<Province> provincesRepository) 
            => this.provincesRepository = provincesRepository;

        public IEnumerable<TViewModel> GetAll<TViewModel>()
            => this.provincesRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>();

        public IEnumerable<TViewModel> GetAllByCountry<TViewModel>(string id)
            => this.provincesRepository
                .All()
                .Where(p => p.CountryId.Equals(id))
                .OrderBy(p => p.Name)
                .ThenByDescending(p => p.Towns.Count)
                .To<TViewModel>();

        public async Task<string> AddAsync(string name, string countryId)
        {
            var province = new Province
            {
                Name = name,
                CountryId = countryId
            };

            await this.provincesRepository.AddAsync(province);
            await this.provincesRepository.SaveChangesAsync();

            return province.Id;
        }

        public async Task RemoveAsync(string id)
        {
            var provinceToRemove = await this.provincesRepository.GetByIdAsync(id);

            if (provinceToRemove == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Province)));
            }

            this.provincesRepository.Delete(provinceToRemove);
            await this.provincesRepository.SaveChangesAsync();
        }

        public TViewModel GetById<TViewModel>(string id) 
            => this.provincesRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .SingleOrDefault();

        public async Task UpdateAsync(string id, string name, string countryId)
        {
            var province = await this.provincesRepository.GetByIdAsync(id);

            if (province == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Province)));
            }

            this.provincesRepository.Update(province);
            await this.provincesRepository.SaveChangesAsync();
        }

        public bool Exist(string id)
            => this.provincesRepository
                .All()
                .Any(x => x.Id == id);
    }
}