namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Versus;

    public class VersusController : Controller
    {
        private readonly IVersusService versusService;

        public VersusController(IVersusService versusService)
        {
            this.versusService = versusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VersusCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var game = Mapper.Map<Versus>(model);

            await this.versusService.AddVersusAsync(game);

            return this.RedirectToAction("Details", new {id = game.Id});
        }


        [HttpGet]
        public IActionResult Details(string id)
        {
            return this.View();
        }
    }
}