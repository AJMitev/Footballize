namespace Footballize.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;
    using Services;
    using ViewModels.Home;

    public class HomeController : ControllerBase
    {
        private const int GamesCount = 5;
        private const int PitchesCount = 3;

        private readonly IGatherService gatherService;
        private readonly IRecruitmentService recruitmentService;
        private readonly IPitchService pitchService;

        public HomeController(IGatherService gatherService, IRecruitmentService recruitmentService, IPitchService pitchService)
        {
            this.gatherService = gatherService;
            this.recruitmentService = recruitmentService;
            this.pitchService = pitchService;
        }

        public IActionResult Index()
        {
            var gathers = this.gatherService.GetAll<HomeGameViewModel>()
                .Take(GamesCount)
                .ToList();

            var recruitments = this.recruitmentService.GetAll<HomeGameViewModel>()
                .Take(GamesCount)
                .ToList();

            var pitches = this.pitchService.GetMostUsed(count: PitchesCount)
                .ToList();

            var model = new HomeIndexViewModel
            {
                Gathers = gathers,
                Recruitments = recruitments,
                Pitches = pitches
            };


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(int? code)
        {
            if (code == 404)
            {
                return this.View("NotFound");
            }


            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
