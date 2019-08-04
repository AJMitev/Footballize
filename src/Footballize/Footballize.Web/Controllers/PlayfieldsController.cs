namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Playfields;
    using Services.Data;

    public class PlayfieldsController : Controller
    {
        private readonly IPlayfieldService playfieldService;
        private readonly IAddressService addressService;

        public PlayfieldsController(IPlayfieldService playfieldService, IAddressService addressService)
        {
            this.playfieldService = playfieldService;
            this.addressService = addressService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var fields = this.playfieldService.GetPlayfileds<PlayfieldIndexViewModel>();

            return this.View(fields);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PlayfiledAddInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var newAddress = Mapper.Map<Address>(model);
            var addressId = await this.addressService.CreateOrGetAddress(newAddress);
            var playfield = Mapper.Map<Playfield>(model);
            playfield.AddressId = addressId;

            await this.playfieldService.AddPlayfiledAsync(playfield);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = this.playfieldService.GetPlayfiled<PlayfieldEditViewModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlayfieldEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                //TODO Return model
                return this.View();
            }
            
            await this.playfieldService.UpdatePlayfieldAsync(Mapper.Map<Playfield>(model));

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            var field = await this.playfieldService.GetPlayfiledAsync(id);

            if (field == null)
            {
                return this.NotFound();
            }

            await this.playfieldService.RemovePlayfieldAsync(field);

            return this.RedirectToAction("Index");
        }
    }
}