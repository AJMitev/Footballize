namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Teams;

    public class TeamsController : Controller
    {
        private readonly ITeamService teamService;
        private readonly UserManager<User> userManager;

        public TeamsController(ITeamService teamService, UserManager<User> userManager)
        {
            this.teamService = teamService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var teams = this.teamService.GetTeams<TeamIndexViewModel>();

            return View(teams);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var newTeam = Mapper.Map<Team>(model);
            newTeam.Owner = await userManager.GetUserAsync(HttpContext.User);
            
            await this.teamService.CreateTeamAsync(newTeam);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            return this.View();
        }
    }
}