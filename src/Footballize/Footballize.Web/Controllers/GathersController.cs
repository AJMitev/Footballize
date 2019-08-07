namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Enums;
    using Services.Data;
    using ViewModels.Gathers;

    [Authorize]
    public class GathersController : Controller
    {
        private readonly IGatherServices gatherServices;
        private readonly UserManager<User> userManager;

        public GathersController(IGatherServices gatherServices, UserManager<User> userManager)
        {
            this.gatherServices = gatherServices;
            this.userManager = userManager;
        }

        [AllowAnonymous]
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
            newGather.Status = GameStatus.Registration;

            switch (newGather.TeamFormat)
            {
                case TeamFormat.FourPlusOne: newGather.MaximumPlayersAllowed = 10; break;
                case TeamFormat.FivePlusOne: newGather.MaximumPlayersAllowed = 12; break;
                case TeamFormat.SixPlusOne: newGather.MaximumPlayersAllowed = 14; break;
                case TeamFormat.ElevenPlayers: newGather.MaximumPlayersAllowed = 22; break;
            }

            await this.gatherServices.AddGatherAsync(newGather);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var gather = this.gatherServices.GetGather<GatherDetailsViewModel>(id);

            if (gather == null)
            {
                return this.NotFound();
            }

            return this.View(gather);
        }

        [HttpGet]
        public async Task<IActionResult> Leave(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);
            await this.gatherServices.LeaveGatherAsync(id, currentUser.Id);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Enroll(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);
            await this.gatherServices.EnrollGatherAsync(id, currentUser.Id);

            return this.RedirectToAction("Details", new {id = id});
        }

        [HttpGet]
        public async Task<IActionResult> Kick(string gatherId, string playerId)
        {
            var gather = this.gatherServices.GetGather<GatherDetailsViewModel>(gatherId);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (gather.Creator != currentUser)
            {
                return this.Forbid();
            }

            await this.gatherServices.LeaveGatherAsync(gatherId, playerId);
            return this.RedirectToAction("Details", new {id = gatherId});
        }

        [HttpGet]
        public async Task<IActionResult> Start(string id)
        {
            await this.gatherServices.StartGather(id);

            return this.RedirectToAction("Details", new {id = id});
        }

        [HttpGet]
        public async Task<IActionResult> Complete(string id)
        {

            return this.RedirectToAction("Details", new {id = id});
        }
    }
}