namespace Footballize.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Footballize.Models;
    using Footballize.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models.Countries;

    public class CountriesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICountryService countryService;

        public CountriesController(IMapper mapper,ICountryService countryService)
        {
            this.mapper = mapper;
            this.countryService = countryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var countriesFromDb = this.countryService.GetCountries();
            var countriesModel = this.mapper.Map<IEnumerable<CountriesIndexViewModel>>(countriesFromDb);
            
            return View(countriesModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CountryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.countryService.AddCountry(this.mapper.Map<Country>(model));

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var dbModel = this.countryService.GetCountry(id);

            if (dbModel == null)
            {
                this.NotFound();
            }


            var model = new CountryInputModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(CountryInputModel model)
        {

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }


            var country = this.countryService.GetCountry(model.Id);
            country.Name = model.Name;

            this.countryService.UpdateCountry(country);

           return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            this.countryService.RemoveCountry(id);
            return this.RedirectToAction("Index");
        }
    }
}