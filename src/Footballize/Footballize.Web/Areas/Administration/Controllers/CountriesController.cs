namespace Footballize.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Countries;

    public class CountriesController : AdminController
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

            await this.countryService.AddCountryAsync(Mapper.Map<Country>(model));

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

            var country = await this.countryService.GetCountryByIdAsync(model.Id);

            if (country == null)
            {
                return this.NotFound();
            }

            Mapper.Map(model, country);

            await this.countryService.UpdateCountryAsync(country);

           return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.countryService.RemoveCountryAsync(id);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var country = this.countryService.GetCountry<CountryDetailsViewModel>(id);

            if (country == null)
            {
                return this.NotFound();
            }


            return this.View(country);
        }
    }
}