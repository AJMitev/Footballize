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
    using Services.Data;
    using Services.Exceptions;
    using ViewModels.Recruitments;

    [Authorize]
    public class RecruitmentsController : Controller
    {
        private const int ItemsPerPage = 10;

        private readonly IRecruitmentService recruitmentService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RecruitmentsController(IRecruitmentService recruitmentService, UserManager<User> userManager,IMapper mapper)
        {
            this.recruitmentService = recruitmentService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(int id)
        {
            id = Math.Max(1, id);
            var skip = (id - 1) * ItemsPerPage;

            var games = this.recruitmentService.GetRecruitments<RecruitmentIndexViewModel>(x => x.StartingAt > DateTime.UtcNow);

            var filtered = games.Skip(skip).Take(ItemsPerPage).ToList();

            var gathersCount = games.Count;
            var pagesCount = (int)Math.Ceiling(gathersCount / (decimal)ItemsPerPage);

            var model = new RecruitmentsListViewModel
            {
                PagesCount = pagesCount,
                CurrentPage = id,
                RecruitmentsCount =  gathersCount,
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

            var newRecruitment = this.mapper.Map<Recruitment>(model);
            newRecruitment.Creator = await userManager.GetUserAsync(HttpContext.User);

            try
            {
                await this.recruitmentService.AddRecruitmentAsync(newRecruitment);

                return this.RedirectToAction("Index");
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var game = this.recruitmentService.GetRecruitment<RecruitmentDetailsViewModel>(id);

            if (game == null)
            {
                return this.NotFound();
            }

            return this.View(game);
        }

        [HttpGet]
        public async Task<IActionResult> Start(string id)
        {
            var game = this.recruitmentService.GetRecruitment<RecruitmentDetailsViewModel>(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser)
            {
                return this.Unauthorized();
            }

            try
            {
                await this.recruitmentService.StartRecruitmentAsync(id);

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
            var game = this.recruitmentService.GetRecruitment<RecruitmentDetailsViewModel>(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser)
            {
                return this.Unauthorized();
            }

            try
            {
                await this.recruitmentService.CompleteRecruitmentAsync(id);

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
            var currentUser = await this.userManager.GetUserAsync(User);
            var game = this.recruitmentService.GetRecruitment<RecruitmentDetailsViewModel>(id);

            if (currentUser == null)
            {
                return this.NotFound();
            }

            try
            {
                await this.recruitmentService.EnrollRecruitmentAsync(game.Id, currentUser);

                return this.RedirectToAction("Details", new { id });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Leave(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);
            var game = await this.recruitmentService.GetRecruitmentAsync(id);

            if (currentUser == null || game == null)
            {
                return this.NotFound();
            }

            try
            {
                await this.recruitmentService.LeaveRecruitmentAsync(game, currentUser.Id);

                return this.RedirectToAction("Details", new { id });

            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Kick(string gameId, string playerId)
        {
            var game = await this.recruitmentService.GetRecruitmentAsync(gameId);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game.Creator != currentUser)
            {
                return this.Unauthorized();
            }

            try
            {
                await this.recruitmentService.LeaveRecruitmentAsync(game, playerId);
                return this.RedirectToAction("Details", new { gameId });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { gameId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var game = await this.recruitmentService.GetRecruitmentAsync(id);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game == null || currentUser == null)
            {
                return this.NotFound();
            }

            if (game.Creator != currentUser && !this.User.IsInRole(GlobalConstants.CanDeleteRecruitmentRoleName))
            {
                return this.Unauthorized();
            }

            await this.recruitmentService.DeleteRecruitmentAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}