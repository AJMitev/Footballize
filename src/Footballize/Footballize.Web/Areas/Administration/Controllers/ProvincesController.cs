namespace Footballize.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Provinces;

    public class ProvincesController : AdminController
    {
        private readonly IProvinceService provinceService;
        private readonly ICountryService countryService;

        public ProvincesController(IProvinceService provinceService, ICountryService countryService)
        {
            this.provinceService = provinceService;
            this.countryService = countryService;
        }


        [HttpGet]
        public IActionResult Add(string countryId)
        {
            if (!this.countryService.Exist(countryId))
            {
                return this.NotFound();
            }

            return this.View(this.countryService.GetById<ProvinceAddViewModel>(countryId));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProvinceAddInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.provinceService.AddAsync(model.Name, model.CountryId);

            return this.RedirectToAction("Details", "Countries", new { id = model.CountryId });
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!this.provinceService.Exist(id))
            {
                return this.NotFound();
            }

            return this.View(this.provinceService.GetById<ProvinceEditViewModel>(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProvinceEditInputModel model)
        {
            if (!ModelState.IsValid || !this.provinceService.Exist(model.Id) || this.countryService.Exist(model.CountryId))
            {
                return this.NotFound();
            }

            await this.provinceService.UpdateAsync(model.Id, model.Name, model.CountryId);

            return this.RedirectToAction("Details", "Countries", new { id = model.CountryId });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.provinceService.Exist(id))
            {
                return this.NotFound();
            }

            await this.provinceService.RemoveAsync(id);

            return this.RedirectToAction("Index", "Countries");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (!this.provinceService.Exist(id))
            {
                return this.NotFound();
            }

            return this.View(this.provinceService.GetById<ProvinceDetailsViewModel>(id));
        }
    }
}