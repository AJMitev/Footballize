namespace Footballize.Web.Controllers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Footballize.Web.ViewModels;
    using Services.Data;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IGatherServices gatherServices;
        private readonly IRecruitmentService recruitmentService;
        private readonly IPitchService pitchService;
        private readonly IMapper mapper;

        public HomeController(IGatherServices gatherServices, IRecruitmentService recruitmentService, IPitchService pitchService, IMapper mapper)
        {
            this.gatherServices = gatherServices;
            this.recruitmentService = recruitmentService;
            this.pitchService = pitchService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {

            var gathers = this.gatherServices.GetGathers<HomeGameViewModel>()
                .Take(8)
                .ToList();

            var recruitments = this.recruitmentService.GetRecruitments<HomeGameViewModel>()
                .Take(8)
                .ToList();

            //var pitches = this.pitchService.GetMostUsedPitches()
                //.Take(4);

            var model = new HomeIndexViewModel
            {
                Gathers = gathers,
                Recruitments = recruitments,
                Pitches = new List<HomePitchViewModel>()
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
