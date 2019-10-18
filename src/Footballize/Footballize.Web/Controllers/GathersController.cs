namespace Footballize.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Enums;
    using Services.Data;
    using Services.Exceptions;
    using ViewModels.Gathers;

    [Authorize]
    public class GathersController : Controller
    {
        private const int ItemsPerPage = 10;

        private readonly IGatherService gatherService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public GathersController(IGatherService gatherService, UserManager<User> userManager, IMapper mapper)
        {
            this.gatherService = gatherService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(int id)
        {
            id = Math.Max(1, id);
            var skip = (id - 1) * ItemsPerPage;


            var gathers = this.gatherService.GetGathers<GatherIndexViewModel>(x => x.StartingAt > DateTime.UtcNow);

            var filtered = gathers.Skip(skip).Take(ItemsPerPage).ToList();

            var gathersCount = gathers.Count;
            var pagesCount = (int)Math.Ceiling(gathersCount / (decimal)ItemsPerPage);

            var model = new GatherListViewModel
            {
                Gathers = filtered,
                PagesCount = pagesCount,
                CurrentPage = id,
                GathersCount =  gathersCount
            };

            return View(model);
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
                return this.View(this.mapper.Map<GatherAddViewModel>(model));
            }

            var newGather = this.mapper.Map<Gather>(model);
            newGather.Creator = await userManager.GetUserAsync(HttpContext.User);
            newGather.Status = GameStatus.Registration;

            switch (newGather.TeamFormat)
            {
                case TeamFormat.FourPlusOne: newGather.MaximumPlayers = 10; break;
                case TeamFormat.FivePlusOne: newGather.MaximumPlayers = 12; break;
                case TeamFormat.SixPlusOne: newGather.MaximumPlayers = 14; break;
                case TeamFormat.ElevenPlayers: newGather.MaximumPlayers = 22; break;
            }

            await this.gatherService.AddGatherAsync(newGather);
            return await this.Enroll(newGather.Id);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var gather = this.gatherService.GetGather<GatherDetailsViewModel>(id);

            if (gather == null)
            {
                return this.NotFound();
            }

            return this.View(gather);
        }

        [HttpGet]
        public async Task<IActionResult> Leave(string id)
        {
            try
            {
                var currentUser = await this.userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return this.Forbid();
                }

                var gather = this.gatherService.GetGatherWithPlayers(id);

                if (gather == null)
                {
                    return this.NotFound();
                }

                await this.gatherService.LeaveGatherAsync(gather, currentUser.Id);

                return this.RedirectToAction("Details", new { id });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Enroll(string id)
        {
            try
            {
                var gather = this.gatherService.GetGatherWithPlayers(id);
                var currentUser = await this.userManager.GetUserAsync(User);

                if (currentUser == null || gather == null)
                {
                    return this.NotFound();
                }

                await this.gatherService.EnrollGatherAsync(gather, currentUser);

                return this.RedirectToAction("Details", new { id });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Kick(string gatherId, string playerId)
        {
            try
            {
                var gather = await this.gatherService.GetGatherAsync(gatherId);
                var currentUser = await this.userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return this.NotFound();
                }

                if (gather.Creator != currentUser)
                {
                    return this.Forbid();
                }

                await this.gatherService.LeaveGatherAsync(gather, playerId);
                return this.RedirectToAction("Details", new { gatherId });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { gatherId });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Start(string id)
        {
            try
            {
                var game = this.gatherService.GetGatherWithPlayers(id);
                var currentUser = await this.userManager.GetUserAsync(User);

                if (game == null || currentUser == null)
                {
                    return this.NotFound();
                }

                if (game.Creator != currentUser)
                {
                    return this.Unauthorized();
                }

                await this.gatherService.StartGatherAsync(id);

                return this.RedirectToAction("Details", new { id });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Complete(string id)
        {
            try
            {
                var game = this.gatherService.GetGather<GatherDetailsViewModel>(id);
                var currentUser = await this.userManager.GetUserAsync(User);

                if (game == null || currentUser == null)
                {
                    return this.NotFound();
                }

                if (game.Creator != currentUser)
                {
                    return this.Unauthorized();
                }

                await this.gatherService.CompleteGatherAsync(id);
                return this.RedirectToAction("Details", new { id });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var game = this.gatherService.GetGather<GatherDetailsViewModel>(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser && !this.User.IsInRole(GlobalConstants.CanDeleteGathersRoleName))
            {
                return this.Unauthorized();
            }

            await this.gatherService.DeleteGatherAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}