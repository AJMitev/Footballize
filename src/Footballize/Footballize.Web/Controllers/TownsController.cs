namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Towns;
    using Services.Data;

    public class TownsController : Controller
    {
        private readonly ITownService townService;

        public TownsController(ITownService townService)
        {
            this.townService = townService;
        }


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
        public IActionResult Add(TownInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(new TownAddViewModel());
            }

            var newTown = Mapper.Map<Town>(model);
            this.townService.AddTown(newTown);

            return this.RedirectToAction("Details", "Provinces", new { id = model.ProvinceId });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var townToEdit = this.townService.GetTown<TownEditViewModel>(id);

            return this.View(townToEdit);
        }

        [HttpPost]
        public async  Task<IActionResult> Edit(TownEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(Mapper.Map<TownEditViewModel>(model));
            }


            await this.townService.UpdateTown(Mapper.Map<Town>(model));
            return this.RedirectToAction("Details", "Provinces", new { id = model.ProvinceId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var provinceId = this.townService.GetTown<TownEditViewModel>(id).ProvinceId;
            await this.townService.DeleteTown(id);

            return this.RedirectToAction("Details", "Provinces", new { id = provinceId });
        }
    }
}
