namespace Footballize.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Towns;

    public class TownsController : AdminController
    {
        private readonly ITownService townService;
        private readonly IMapper mapper;

        public TownsController(ITownService townService, IMapper mapper)
        {
            this.townService = townService;
            this.mapper = mapper;
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

            var newTown = this.mapper.Map<Town>(model);
            this.townService.AddTownAsync(newTown);

            return this.RedirectToAction("Details", "Provinces", new { id = model.ProvinceId });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var townToEdit = this.townService.GetTown<TownEditViewModel>(id);

            if (townToEdit ==  null)
            {
                return this.NotFound();
            }

            return this.View(townToEdit);
        }

        [HttpPost]
        public async  Task<IActionResult> Edit(TownEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(this.mapper.Map<TownEditViewModel>(model));
            }


            await this.townService.UpdateTownAsync(this.mapper.Map<Town>(model));
            return this.RedirectToAction("Details", "Provinces", new { id = model.ProvinceId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var provinceId = this.townService.GetTown<TownEditViewModel>(id).ProvinceId;

            if (provinceId ==  null)
            {
                return this.NotFound();
            }

            await this.townService.DeleteTownAsync(id);

            return this.RedirectToAction("Details", "Provinces", new { id = provinceId });
        }
    }
}
