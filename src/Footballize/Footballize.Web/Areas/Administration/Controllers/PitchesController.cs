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
    using ViewModels.Pitches;

    public class PitchesController : AdminController
    {
        private readonly IPitchService pitchService;
        private readonly IAddressService addressService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IMapper mapper;

        public PitchesController(IPitchService pitchService, IAddressService addressService, 
            IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            this.pitchService = pitchService;
            this.addressService = addressService;
            this.hostingEnvironment = hostingEnvironment;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var fields = this.pitchService.GetAll<PitchIndexViewModel>().ToList();
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

            string addressId;
            if (this.addressService.Exists(model.Street, model.Number))
            {
                addressId = this.addressService.GetByName(model.Street, model.Number).Id;
            }
            else
            {
                addressId = await this.addressService.Create(model.Street, model.Number, model.Latitude, model.Longitude);
            }
            
            var playfield = this.mapper.Map<Pitch>(model);
            playfield.AddressId = addressId;

            await this.pitchService.AddAsync(playfield);

            // Extract this to separate service
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
            var model = this.pitchService.GetById<PitchEditViewModel>(id);

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
                return this.View(this.mapper.Map<PitchEditViewModel>(model));
            }
            
            await this.pitchService.UpdateAsync(this.mapper.Map<Pitch>(model));

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

            var field =  this.pitchService.GetById<PitchNameAndIdViewModel>(id);

            if (field == null)
            {
                return this.NotFound();
            }

            await this.pitchService.RemoveAsync(this.mapper.Map<Pitch>(field));

            return this.RedirectToAction("Index");
        }
    }
}