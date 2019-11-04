namespace Footballize.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Models.Enums;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using ViewModels.Recruitments;

    [Authorize]
    public class RecruitmentsController : ControllerBase
    {
        private const int ItemsPerPage = 10;

        private readonly IRecruitmentService recruitmentService;
        private readonly UserManager<User> userManager;

        public RecruitmentsController(IRecruitmentService recruitmentService, UserManager<User> userManager)
        {
            this.recruitmentService = recruitmentService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(int id)
        {
            id = Math.Max(1, id);
            var skip = (id - 1) * ItemsPerPage;

            var games = this.recruitmentService.GetAll<RecruitmentIndexViewModel>(x => x.StartingAt > DateTime.UtcNow);

            var filtered = games.Skip(skip)
                .Take(ItemsPerPage)
                .ToList();

            var gathersCount = games.Count();
            var pagesCount = (int)Math.Ceiling(gathersCount / (decimal)ItemsPerPage);

            var model = new RecruitmentsListViewModel
            {
                PagesCount = pagesCount,
                CurrentPage = id,
                RecruitmentsCount = gathersCount,
                Recruitments = filtered
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecruitmentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var id = await this.recruitmentService.AddAsync(model.Title, model.StartingAt, model.PitchId, this.User.GetId(), model.MaximumPlayers);

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var game = this.recruitmentService.GetById<RecruitmentDetailsViewModel>(id);

            if (game == null)
            {
                return this.NotFound();
            }

            return this.View(game);
        }

        [HttpGet]
        public async Task<IActionResult> Start(string id)
        {

            if (!this.recruitmentService.Exists(id))
            {
                return this.NotFound();

            }

            var game = await this.recruitmentService.GetByIdAsync(id);


            if (game.CreatorId != this.User.GetId())
            {
                return this.Unauthorized();
            }

            if (game.Players.Count != game.MaximumPlayers)
            {
                this.DisplayError(GlobalConstants.RequiredNumberOfPlayersNotReachedErrorMessage);

                return this.RedirectToAction(nameof(Details), new { id });
            }

            if (game.Status != GameStatus.Registration)
            {
                this.DisplayError(GlobalConstants.ThisGameIsAlreadyStartedErrorMessage);

                return this.RedirectToAction(nameof(Details), new { id });
            }


            await this.recruitmentService.StartAsync(id);

            return this.RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Complete(string id)
        {
            var game = this.recruitmentService.GetById<RecruitmentDetailsViewModel>(id);

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

            await this.recruitmentService.CompleteAsync(id);

            return this.RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Enroll(string id)
        {
            var recruitment = this.recruitmentService.GetById<RecruitmentDetailsViewModel>(id);
            var user = await this.userManager.GetUserAsync(this.User);

            if (recruitment.Status != GameStatus.Registration || recruitment.Players.Count >= recruitment.MaximumPlayers)
            {
                this.DisplayError(GlobalConstants.NotInRegistrationOrNoFreeSlotErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            if (user.IsBanned)
            {
                this.DisplayError(GlobalConstants.PlayerIsBannedErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            if (recruitment.Players.Any(x => x.Id == user.Id))
            {
                this.DisplayError(GlobalConstants.AlreadyJoinedErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            await this.recruitmentService.EnrollAsync(recruitment.Id, this.User.GetId());
            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Leave(string id)
        {
            if (!this.recruitmentService.Exists(id))
            {
                return this.NotFound();
            }
            var game = await this.recruitmentService.GetByIdAsync(id);

            if (game == null)
            {
                return this.NotFound();
            }

            if (game.Status != GameStatus.Registration)
            {
                this.DisplayError(GlobalConstants.KickPlayerOnlyInRegistrationModeErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            if (game.Players.All(x => x.Id != User.GetId()))
            {
                this.DisplayError(GlobalConstants.PlayerIsNotPartOfTheGameErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            await this.recruitmentService.LeaveAsync(id, this.User.GetId());

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Kick(string gameId, string playerId)
        {
            if (!this.recruitmentService.Exists(gameId))
            {
                return this.NotFound();
            }

            var game = await this.recruitmentService.GetByIdAsync(gameId);

            if (game.CreatorId != this.User.GetId())
            {
                return this.Unauthorized();
            }

            if (game.Status != GameStatus.Registration)
            {
                this.DisplayError(GlobalConstants.KickPlayerOnlyInRegistrationModeErrorMessage);
                return this.RedirectToAction(nameof(Details), new { id = gameId });
            }

            await this.recruitmentService.LeaveAsync(gameId, playerId);
            return this.RedirectToAction(nameof(Details), new { id = gameId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.recruitmentService.Exists(id))
            {
                return this.NotFound();
            }

            var game = await this.recruitmentService.GetByIdAsync(id);

            if (game.CreatorId != this.User.GetId() && !this.User.IsInRole(GlobalConstants.CanDeleteRecruitmentRoleName))
            {
                return this.Unauthorized();
            }

            await this.recruitmentService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}