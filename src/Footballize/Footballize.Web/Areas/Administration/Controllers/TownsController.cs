namespace Footballize.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Towns;

    public class TownsController : AdminController
    {
        private readonly ITownService townService;

        public TownsController(ITownService townService)
            => this.townService = townService;


        [HttpGet]
        public IActionResult Add(string countryId, string provinceId)
        {
            var model = new TownAddViewModel
            {
                CountryId = countryId,
                ProvinceId = provinceId
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(TownAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.townService.AddAsync(model.Name, model.ProvinceId);

            return this.RedirectToAction("Details", "Provinces", new { id = model.ProvinceId });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!this.townService.Exists(id))
            {
                return this.NotFound();
            }

            return this.View(this.townService.GetById<TownEditViewModel>(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TownEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.townService.Exists(model.Id))
            {
                this.NotFound();
            }

            await this.townService.UpdateAsync(model.Id, model.Name, model.ProvinceId);
            return this.RedirectToAction("Details", "Provinces", new { id = model.ProvinceId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.townService.Exists(id))
            {
                return this.NotFound();
            }

            await this.townService.DeleteAsync(id);

            return this.RedirectToAction("Details", "Provinces", new { id = await this.townService.GetProvinceId(id) });
        }
    }
}
