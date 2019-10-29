namespace Footballize.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using ViewModels.Countries;

    public class CountriesController : AdminController
    {
        private readonly ICountryService countryService;
        private readonly IMapper mapper;

        public CountriesController(ICountryService countryService, IMapper mapper)
        {
            this.countryService = countryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var countries = this.countryService.All<CountryIndexViewModel>();

            id = Math.Max(1, id);
            var skip = (id - 1) * CountriesListViewModel.ItemsPerPage;

            var filteredItems = countries
                .Skip(skip)
                .Take(CountriesListViewModel.ItemsPerPage)
                .ToList();

            var itemsCount = countries.Count();
            var pagesCount = (int)Math.Ceiling(itemsCount / (decimal)CountriesListViewModel.ItemsPerPage);

            var model = new CountriesListViewModel
            {
                Items = filteredItems,
                ItemsCount = itemsCount,
                CurrentPage = id,
                PagesCount = pagesCount
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
            => this.View();

        [HttpPost]
        public async Task<IActionResult> Add(CountryInputModel model)
        {
            if (!ModelState.IsValid)
                return this.View(model);

            var id = await this.countryService.AddAsync(model.Name, model.IsoCode);

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = this.countryService.GetById<CountryInputModel>(id);

            if (model == null)
                this.NotFound();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryInputModel model)
        {
            if (!ModelState.IsValid)
                return this.View(model);

            var country = await this.countryService.GetByIdAsync(model.Id);

            if (country == null)
            {
                return this.NotFound();
            }

            this.mapper.Map(model, country);

            await this.countryService.UpdateAsync(model.Id, model.Name, model.IsoCode);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.countryService.DeleteAsync(id);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var country = this.countryService.GetById<CountryDetailsViewModel>(id);

            if (country == null)
            {
                return this.NotFound();
            }


            return this.View(country);
        }
    }
}