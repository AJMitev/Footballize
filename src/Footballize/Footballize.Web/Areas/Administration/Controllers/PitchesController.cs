namespace Footballize.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Pitches;

    public class PitchesController : AdminController
    {
        private readonly IPitchService pitchService;
        private readonly IAddressService addressService;
        private readonly string pitchCoverHostingPath;

        public PitchesController(IPitchService pitchService, IAddressService addressService, IWebHostEnvironment hostingEnvironment)
        {
            this.pitchService = pitchService;
            this.addressService = addressService;
            this.pitchCoverHostingPath = $"{hostingEnvironment.WebRootPath}/img/fields/";
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var fields = this.pitchService.GetAll<PitchIndexViewModel>()
                .ToList();
            id = Math.Max(1, id);
            var skip = (id - 1) * PitchesListViewModel.ItemsPerPage;

            var filteredItems = fields.Skip(skip)
                .Take(PitchesListViewModel.ItemsPerPage)
                .ToList();

            var fieldsCount = fields.Count;
            var pagesCount = (int)Math.Ceiling(fieldsCount / (decimal)PitchesListViewModel.ItemsPerPage);

            var model = new PitchesListViewModel
            {
                Items = filteredItems,
                ItemsCount = fieldsCount,
                CurrentPage = id,
                PagesCount = pagesCount
            };


            return this.View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PitchAddInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            string addressId;
            if (this.addressService.Exists(model.Street, model.Number))
            {
                addressId = this.addressService.GetByName(model.Street, model.Number).Id;
            }
            else
            {
                addressId = await this.addressService.AddAsync(model.Street, model.Number, model.Latitude, model.Longitude);
            }

            if (!this.pitchService.Exist(model.Name, addressId))
            {
                var pitchId = await this.pitchService.AddAsync(model.Name, addressId);
                await this.pitchService.SaveCoverAsync(pitchId, model.Cover, this.pitchCoverHostingPath);
            }


            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (this.pitchService.Exist(id))
            {
                return this.View(this.pitchService.GetById<PitchEditViewModel>(id));
            }

            return this.NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PitchEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.pitchService.UpdateAsync(model.Id, model.Name, model.Address.Id);
            await this.pitchService.SaveCoverAsync(model.Id, model.Cover, this.pitchCoverHostingPath);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.pitchService.Exist(id))
            {
                return this.NotFound();
            }

            await this.pitchService.RemoveAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}