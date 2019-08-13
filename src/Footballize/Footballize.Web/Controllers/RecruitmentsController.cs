namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Recruitments;

    public class RecruitmentsController : Controller
    {
        private readonly IRecruitmentService recruitmentService;
        private readonly UserManager<User> userManager;

        public RecruitmentsController(IRecruitmentService recruitmentService, UserManager<User> userManager)
        {
            this.recruitmentService = recruitmentService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var recruitments = this.recruitmentService.GetRecruitments<RecruitmentIndexViewModel>();

            return View(recruitments);
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

            var newRecruitment = Mapper.Map<Recruitment>(model);
            newRecruitment.Creator = await userManager.GetUserAsync(HttpContext.User);

            await this.recruitmentService.AddRecruitmentAsync(newRecruitment);

            return this.RedirectToAction("Index");
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


            await this.recruitmentService.StartRecruitmentAsync(id);

            return this.RedirectToAction("Details", new { id = id });
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


            await this.recruitmentService.CompleteRecruitmentAsync(id);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Enroll(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return this.NotFound();
            }

            await this.recruitmentService.EnrollRecruitmentAsync(id, currentUser.Id);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Leave(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return this.NotFound();
            }

            await this.recruitmentService.LeaveRecruitmentAsync(id, currentUser.Id);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Kick(string gameId, string playerId)
        {
            var game = this.recruitmentService.GetRecruitment<RecruitmentDetailsViewModel>(gameId);
            var currentUser = await this.userManager.GetUserAsync(User);

            if (game.Creator != currentUser)
            {
                return this.Unauthorized();
            }

            await this.recruitmentService.LeaveRecruitmentAsync(gameId, playerId);
            return this.RedirectToAction("Details", new { id = gameId });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
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


            await this.recruitmentService.DeleteRecruitmentAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}