namespace Footballize.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Countries;

    public class CountriesController : AdminController
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
            => this.countryService = countryService;

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
            {
                return this.View(model);
            }

            var id = await this.countryService.AddAsync(model.Name, model.IsoCode);

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!this.countryService.Exist(id))
            {
                return this.NotFound();
            }

            return this.View(this.countryService.GetById<CountryInputModel>(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.countryService.Exist(model.Id))
            {
                return this.NotFound();
            }

            await this.countryService.UpdateAsync(model.Id, model.Name, model.IsoCode);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.countryService.Exist(id))
            {
                return this.NotFound();
            }

            await this.countryService.DeleteAsync(id);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (!this.countryService.Exist(id))
            {
                return this.NotFound();
            }
            
            return this.View(this.countryService.GetById<CountryDetailsViewModel>(id));
        }
    }
}