namespace Footballize.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using ViewModels.Gathers;

    public class GathersController : Controller
    {
        private readonly IGatherServices gatherServices;

        public GathersController(IGatherServices gatherServices)
        {
            this.gatherServices = gatherServices;
        }

        public IActionResult Index()
        {
            var gathers = this.gatherServices.GetGathers<GatherIndexViewModel>();

            return View(gathers);
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}