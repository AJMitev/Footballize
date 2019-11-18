namespace Footballize.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Enums;
    using Services;
    using ViewModels.Gathers;

    [Authorize]
    public class GathersController : ControllerBase
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


            var gathers = this.gatherService.GetAll<GatherIndexViewModel>(x => x.StartingAt > DateTime.UtcNow);

            var filtered = gathers.Skip(skip)
                .Take(ItemsPerPage)
                .ToList();

            var gathersCount = gathers.Count();
            var pagesCount = (int)Math.Ceiling(gathersCount / (decimal)ItemsPerPage);

            var model = new GatherListViewModel
            {
                Gathers = filtered,
                PagesCount = pagesCount,
                CurrentPage = id,
                GathersCount = gathersCount
            };

            return this.View(model);
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

            var gatherId = await this.gatherService.AddAsync(model.Title, model.Description, model.StartingAt, model.TeamFormat, model.PitchId, this.User.GetId());

            return await this.Enroll(gatherId);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var gather = this.gatherService.GetById<GatherDetailsViewModel>(id);

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

            var gather = await this.gatherService.GetByIdAsync(id);

            if (gather == null)
            {
                return this.NotFound();
            }

            if (gather.Status != GameStatus.Registration)
            {

                this.DisplayError(GlobalConstants.KickPlayerOnlyInRegistrationModeErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            if (gather.Players.All(x => x.UserId != currentUser.Id))
            {
                this.DisplayError(GlobalConstants.InvalidRequestParametersErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            await this.gatherService.LeaveAsync(gather.Id, currentUser.Id);

            return this.RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Enroll(string id)
        {
            var gather = await this.gatherService.GetByIdAsync(id);
            var user = await this.userManager.GetUserAsync(User);

            if (user == null || gather == null)
            {
                return this.NotFound();
            }

            if (gather.Status != GameStatus.Registration || gather.Players.Count >= gather.MaximumPlayers)
            {
                this.DisplayError(GlobalConstants.NotInRegistrationOrNoFreeSlotErrorMessage);
            }

            if (user.IsBanned)
            {
                this.DisplayError(GlobalConstants.PlayerIsBannedErrorMessage);
            }

            if (gather.Players.Any(x => x.UserId == user.Id))
            {
                this.DisplayError(GlobalConstants.AlreadyJoinedErrorMessage);
            }

            await this.gatherService.EnrollAsync(gather.Id, user.Id);

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Kick(string gatherId, string playerId)
        {
            var gather = await this.gatherService.GetByIdAsync(gatherId);

            if (gather == null)
            {
                return this.NotFound();
            }

            if (gather.CreatorId != this.User.GetId())
            {
                return this.Forbid();
            }

            if (gather.Status != GameStatus.Registration)
            {

                this.DisplayError(GlobalConstants.KickPlayerOnlyInRegistrationModeErrorMessage);
                return this.RedirectToAction(nameof(Details), new { gatherId });
            }

            if (gather.Players.All(x => x.UserId != playerId))
            {
                this.DisplayError(GlobalConstants.InvalidRequestParametersErrorMessage);
                return this.RedirectToAction(nameof(Details), new { gatherId });
            }

            await this.gatherService.LeaveAsync(gather.Id, playerId);
            return this.RedirectToAction(nameof(Details), new { gatherId });
        }

        [HttpGet]
        public async Task<IActionResult> Start(string id)
        {
            var gather = await this.gatherService.GetByIdAsync(id);

            if (gather == null)
            {
                return this.NotFound();
            }

            if (gather.CreatorId != this.User.GetId())
            {
                return this.Unauthorized();
            }

            if (gather.Players.Count != gather.MaximumPlayers)
            {
                this.DisplayError(GlobalConstants.RequiredNumberOfPlayersNotReachedErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            if (gather.Status != GameStatus.Registration)
            {
                this.DisplayError( GlobalConstants.ThisGameIsAlreadyStartedErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }


            await this.gatherService.StartAsync(id);
            return this.RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Complete(string id)
        {
                var game = this.gatherService.GetById<GatherDetailsViewModel>(id);

                if (game == null)
                {
                    return this.NotFound();
                }

                if (game.Creator.Id != this.User.GetId())
                {
                    return this.Unauthorized();
                }

                if (game.Status != GameStatus.Started)
                {
                    this.DisplayError(GlobalConstants.ThisGameIsNotStartedYetErrorMessage);
                    return this.RedirectToAction(nameof(Details), new { id });
                }

                await this.gatherService.CompleteAsync(id);
                return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var game = this.gatherService.GetById<GatherDetailsViewModel>(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser && !this.User.IsInRole(GlobalConstants.CanDeleteGathersRoleName))
            {
                return this.Unauthorized();
            }

            await this.gatherService.DeleteAsync(id);

            return this.RedirectToAction(nameof(Index));
        }
    }
}