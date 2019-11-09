namespace Footballize.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Administration.ViewModels.Towns;
    using Data.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class TownsController : ApiController
    {
        private readonly ITownService townService;
        private readonly IRepository<Pitch> pitchRepository;

        public TownsController(ITownService townService, IRepository<Pitch> pitchRepository)
        {
            this.townService = townService;
            this.pitchRepository = pitchRepository;
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public IEnumerable<TownWithProvinceViewModel> Get(string id)
        {
            var towns = this.pitchRepository.All()
                .Where(x => x.Address.Town.Province.CountryId == id)
                
                .Select(x => new TownWithProvinceViewModel
                {
                    Id = x.Address.TownId,
                    Name = string.Concat(x.Address.Town.Name, ", ", x.Address.Town.Province.Name)
                })
                .Distinct()
                .ToList();

            return towns;
        }

        [HttpGet("all/{id}")]
        public IEnumerable<TownWithProvinceViewModel> GetAll(string id)
        {
            return this.townService.GetByCountryId<TownWithProvinceViewModel>(id);
        }
    }
}
