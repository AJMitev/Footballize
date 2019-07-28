namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Countries;
    using Services.Data;
    using ViewModels.Countries;

    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var countries = this.countryService.GetCountries<CountriesIndexViewModel>();
            
            return View(countries);
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
                return this.View(model);

            await this.countryService.AddCountry(Mapper.Map<Country>(model));

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = this.countryService.GetCountry<CountryInputModel>(id);

            if (model == null)
                this.NotFound();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryInputModel model)
        {

            if (!ModelState.IsValid)
                return this.View(model);

            var country = await this.countryService.GetCountryById(model.Id);
            country.Name = model.Name;
            country.IsoCode = model.IsoCode;


            await this.countryService.UpdateCountry(country);

           return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.countryService.RemoveCountry(id);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            return this.View();
        }
    }
}