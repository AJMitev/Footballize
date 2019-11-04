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

    public class TownService : ITownService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownService(IDeletableEntityRepository<Town> townRepository) 
            => this.townRepository = townRepository;

        public async Task DeleteAsync(string id)
        {
            var townToDelete = await this.townRepository.GetByIdAsync(id);

            if (townToDelete == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Town)));
            }
            this.townRepository.Delete(townToDelete);
            await this.townRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string townId, string name, string provinceId)
        {
            var town = await this.townRepository.GetByIdAsync(townId);

            town.Name = name;
            town.ProvinceId = provinceId;

            this.townRepository.Update(town);
            await this.townRepository.SaveChangesAsync();
        }

        public bool Exists(string id)
            => this.townRepository
                .All()
                .Any(x => x.Id == id);

        public async Task<string> AddAsync(string name, string provinceId)
        {
           var town = new Town
           {
               Name = name,
               ProvinceId = provinceId
           };

           await this.townRepository.AddAsync(town);
           await this.townRepository.SaveChangesAsync();

           return town.Id;
        }

        public TViewModel GetById<TViewModel>(string id)
        {
            return this.townRepository
                .All()
                .Where(x=>x.Id == id)
                .To<TViewModel>()
                .SingleOrDefault();
        }

        public IEnumerable<TViewModel> GetByCountryId<TViewModel>(string countryId)
        {
            return this.townRepository
                .All()
                .Where(p => p.Province.CountryId.Equals(countryId))
                .OrderBy(p => p.Name)
                .To<TViewModel>();
        }

        public async Task UpdateAsync(Town town)
        {
            if (town == null)
            {
                throw new ServiceException(
                    string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Town)));
            }

            this.townRepository.Update(town);
            await this.townRepository.SaveChangesAsync();
        }
    }
}