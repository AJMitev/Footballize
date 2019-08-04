namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Enums;
    using Services.Data;
    using ViewModels.Gathers;

    public class GathersController : Controller
    {
        private readonly IGatherServices gatherServices;
        private readonly UserManager<User> userManager;

        public GathersController(IGatherServices gatherServices, UserManager<User> userManager)
        {
            this.gatherServices = gatherServices;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var gathers = this.gatherServices.GetGathers<GatherIndexViewModel>();

            return View(gathers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GatherAddInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(Mapper.Map<GatherAddViewModel>(model));
            }

            var newGather = Mapper.Map<Gather>(model);
            newGather.Creator = await userManager.GetUserAsync(HttpContext.User);
            this.SetMaxAllowedPlayers(newGather);

            await this.gatherServices.AddGatherAsync(newGather);
            return this.RedirectToAction("Index");
        }

        private void SetMaxAllowedPlayers(Gather newGather)
        {
            switch (newGather.TeamFormat)
            {
                case TeamFormat.FourPlusOne: newGather.MaximumPlayersAllowed = 10; break;
                case TeamFormat.FivePlusOne: newGather.MaximumPlayersAllowed = 12; break;
                case TeamFormat.SixPlusOne: newGather.MaximumPlayersAllowed = 14; break;
                case TeamFormat.ElevenPlayers: newGather.MaximumPlayersAllowed = 22; break;
            }
        }
    }
}