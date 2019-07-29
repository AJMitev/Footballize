namespace Footballize.Web.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Repositories;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Towns;
    using Services.Data;
    using ViewModels.Countries;

    public class TownsController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ITownService townService;

        public TownsController(ICountryService countryService, ITownService townService)
        {
            this.countryService = countryService;
            this.townService = townService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new TownAddViewModel();

            return this.View(model);
        }

        private TownAddViewModel GetTownViewModel()
        {
            var countries = this.countryService.GetCountries<CountryNameAndIdViewModel>();

            var model = new TownAddViewModel
            {
                Countries = new SelectList(countries, nameof(Country.Id), nameof(Country.Name)),
                Provinces = null,
            };
            return model;
        }

        [HttpPost]
        public IActionResult Add(TownInputModel model)
        {
            if (!ModelState.IsValid)
            {
                //var model = GetTownViewModel();
                return this.View(model);
            }



            return this.RedirectToAction("Index");
        }
    }
}
