namespace Footballize.Web.Controllers
{
    using System.Linq;
    using Footballize.Models;
    using Footballize.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Server.IIS.Core;
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
                .Select(x => new CountriesIndexViewModel
                {
                    Name = x.Name,
                    Id = x.Id
                });

            return View(countries);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CountryInputModel model)
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