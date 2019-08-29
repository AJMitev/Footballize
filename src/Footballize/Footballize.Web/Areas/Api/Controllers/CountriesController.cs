namespace Footballize.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Administration.ViewModels.Countries;
    using Data.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;

    public class CountriesController : ApiController
    {
        private readonly ICountryService countryService;
        private readonly IRepository<Pitch> pitchRepository;

        public CountriesController(ICountryService countryService, IRepository<Pitch> pitchRepository)
        {
            this.countryService = countryService;
            this.pitchRepository = pitchRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public IEnumerable<CountryNameAndIdViewModel> Get()
        {
            var countriesWithFields = this.pitchRepository.All()
                .Select(x => new CountryNameAndIdViewModel
                {
                    Id = x.Address.Town.Province.CountryId,
                    Name = x.Address.Town.Province.Country.Name
                })
                .Distinct()
                .ToList();

            return countriesWithFields;
        }

        [HttpGet("all/")]
        public IEnumerable<CountryNameAndIdViewModel> GetAll(string id)
        {
            return this.countryService.GetCountries<CountryNameAndIdViewModel>();
        }
    }
}
