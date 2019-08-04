namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class TownService : ITownService
    {
        private readonly IRepository<Town> townRepository;

        public TownService(IRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }

        public async Task AddTown(Town town)
        {
            if (town == null)
            {
                throw new NullReferenceException();
            }

            await this.townRepository.AddAsync(town);
            await this.townRepository.SaveChangesAsync();
        }

        public async Task DeleteTown(string id)
        {
            var townToDelete = await this.townRepository.GetByIdAsync(id);
            this.townRepository.Delete(townToDelete);
            await this.townRepository.SaveChangesAsync();
        }

        public TViewModel GetTown<TViewModel>(string id)
        {
            return this.townRepository
                .All()
                .Where(x=>x.Id == id)
                .To<TViewModel>()
                .SingleOrDefault();
        }

        public IEnumerable<TViewModel> GetTownsByProvince<TViewModel>(string countryId)
        {
            return this.townRepository
                .All()
                .Where(p => p.ProvinceId.Equals(countryId))
                .OrderBy(p => p.Name)
                .To<TViewModel>();
        }

        public async Task UpdateTown(Town town)
        {
            this.townRepository.Update(town);
            await this.townRepository.SaveChangesAsync();
        }
    }
}