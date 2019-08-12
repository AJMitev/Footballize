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

        public RecruitmentsController(IRecruitmentService recruitmentService,UserManager<User> userManager)
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
    }
}