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
                case TeamFormat.FourPlusOne: newGather.MaximumPlayers = 10; break;
                case TeamFormat.FivePlusOne: newGather.MaximumPlayers = 12; break;
                case TeamFormat.SixPlusOne: newGather.MaximumPlayers = 14; break;
                case TeamFormat.ElevenPlayers: newGather.MaximumPlayers = 22; break;
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

            if (currentUser == null)
            {
                return this.Forbid();
            }

            var gather = await this.gatherServices.GetGatherAsync(id);

            if (gather == null)
            {
                return this.NotFound();
            }


            await this.gatherServices.LeaveGatherAsync(gather, currentUser.Id);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Enroll(string id)
        {
            var gather = await this.gatherServices.GetGatherAsync(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser == null || gather == null)
            {
                return this.NotFound();
            }

            await this.gatherServices.EnrollGatherAsync(gather, currentUser);

            return this.RedirectToAction("Details", new {id = id});
        }

        [HttpGet]
        public async Task<IActionResult> Kick(string gatherId, string playerId)
        {
            var gather = await this.gatherServices.GetGatherAsync(gatherId);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return this.NotFound();
            }

            if (gather.Creator != currentUser || gather.Status != GameStatus.Registration)
            {
                return this.Forbid();
            }

            await this.gatherServices.LeaveGatherAsync(gather, playerId);
            return this.RedirectToAction("Details", new {id = gatherId});
        }

        [HttpGet]
        public async Task<IActionResult> Start(string id)
        {
            var game = this.gatherServices.GetGather<GatherDetailsViewModel>(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser)
            {
                return this.Unauthorized();
            }

            await this.gatherServices.StartGather(id);

            return this.RedirectToAction("Details", new {id = id});
        }

        [HttpGet]
        public async Task<IActionResult> Complete(string id)
        {
            var game = this.gatherServices.GetGather<GatherDetailsViewModel>(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser)
            {
                return this.Unauthorized();
            }

            await this.gatherServices.CompleteGather(id);
            return this.RedirectToAction("Details", new {id = id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var game = this.gatherServices.GetGather<GatherDetailsViewModel>(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser)
            {
                return this.Unauthorized();
            }
            
            await this.gatherServices.DeleteGatherAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}