namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class TownService : ITownService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownService(IDeletableEntityRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }

        public async Task AddTownAsync(Town town)
        {
            if (town == null)
            {
                throw new ServiceException(string.Format(GlobalConstants.EntityCannotBeNullErrorMessage, nameof(Town)));
            }

            await this.townRepository.AddAsync(town);
            await this.townRepository.SaveChangesAsync();
        }

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