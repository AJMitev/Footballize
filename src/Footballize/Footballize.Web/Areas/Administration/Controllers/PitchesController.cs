namespace Footballize.Web.Areas.Administration.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Dashboard;
    using ViewModels.Pitches;

    public class PitchesController : AdminController
    {
        private readonly IPitchService _pitchService;
        private readonly IAddressService addressService;
        private readonly IHostingEnvironment hostingEnvironment;

        public PitchesController(IPitchService pitchService, IAddressService addressService, IHostingEnvironment hostingEnvironment)
        {
            this._pitchService = pitchService;
            this.addressService = addressService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var fields = this._pitchService.GetPitches<PitchIndexViewModel>().ToList();
            id = Math.Max(1, id);
            var skip = (id - 1) * PitchesListViewModel.ItemsPerPage;

            var filteredItems = fields.Skip(skip).Take(PitchesListViewModel.ItemsPerPage).ToList();

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

            var location = Mapper.Map<Location>(model);
            var newAddress = Mapper.Map<Address>(model);
            newAddress.Location = location;
            var addressId = await this.addressService.CreateOrGetAddress(newAddress);
            var playfield = Mapper.Map<Pitch>(model);
            playfield.AddressId = addressId;
            await this._pitchService.AddPitchAsync(playfield);

            var folderPath = hostingEnvironment.WebRootPath + "/img/fields/";
            Directory.CreateDirectory(folderPath);


            using (var stream = System.IO.File.OpenWrite(hostingEnvironment.WebRootPath +$"/img/fields/{playfield.Id}.jpg"))
            {
                await model.Cover.CopyToAsync(stream);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = this._pitchService.GetPitch<PitchEditViewModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PitchEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(Mapper.Map<PitchEditViewModel>(model));
            }
            
            await this._pitchService.UpdatePitchAsync(Mapper.Map<Pitch>(model));

            var folderPath = hostingEnvironment.WebRootPath + "/img/fields/";
            Directory.CreateDirectory(folderPath);


            using (var stream = System.IO.File.OpenWrite(hostingEnvironment.WebRootPath +$"/img/fields/{model.Id}.jpg"))
            {
                await model.Cover.CopyToAsync(stream);
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            var field = await this._pitchService.GetPitchAsync(id);

            if (field == null)
            {
                return this.NotFound();
            }

            await this._pitchService.RemovePitchAsync(field);

            return this.RedirectToAction("Index");
        }
    }
}