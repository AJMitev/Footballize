namespace Footballize.Web.Controllers
{
    using System.Linq;
    using Footballize.Models;
    using Footballize.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models.Countries;

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
            var countries = this.countryService.GetCountries()
                .Select(x=> new CountriesIndexViewModel{Name = x.Name});

            return View(countries);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CountryAddInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var country = new Country
            {
                Name = model.Name
            };

            this.countryService.AddCountry(country);

            return this.RedirectToAction("Index");
        }
    }
}