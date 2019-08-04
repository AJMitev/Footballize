namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using Footballize.Web.ViewModels.Pitches;

    public class PitchesController : Controller
    {
        private readonly IPitchService _pitchService;
        private readonly IAddressService addressService;

        public PitchesController(IPitchService pitchService, IAddressService addressService)
        {
            this._pitchService = pitchService;
            this.addressService = addressService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var fields = this._pitchService.GetPitches<PitchIndexViewModel>();

            return this.View(fields);
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

            var newAddress = Mapper.Map<Address>(model);
            var addressId = await this.addressService.CreateOrGetAddress(newAddress);
            var playfield = Mapper.Map<Pitch>(model);
            playfield.AddressId = addressId;

            await this._pitchService.AddPitchAsync(playfield);

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
                //TODO Return model
                return this.View();
            }
            
            await this._pitchService.UpdatePitchAsync(Mapper.Map<Pitch>(model));

            return this.RedirectToAction("Index");
        }

        [HttpGet]
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