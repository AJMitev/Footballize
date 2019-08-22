namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Teams;

    [Authorize]
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
            var team = this.teamService.GetTeam<TeamDetailsViewModel>(id);

            if (team == null)
            {
                return this.NotFound();
            }

            return this.View(team);
        }

        [HttpGet]
        public IActionResult Kick(string id)
        {
            //TODO: Implement me.
            return this.RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var team =  await this.teamService.GetTeamAsync(id);

            if (team == null)
            {
                return this.NotFound();
            }

            await this.teamService.DeleteTeamAsync(team);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Join(TeamJoinInputModel model)
        {
            if (ModelState.IsValid)
            {
                var team =  this.teamService.GetTeamWithPlayers(model.Id);
                var user = await userManager.GetUserAsync(HttpContext.User);

                if (team == null || user == null)
                {
                    return this.NotFound();
                }

                await teamService.JoinTeamAsync(team, model.Password, user);
            }


            return this.RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Leave(string id)
        {
            var teamToLeave =  this.teamService.GetTeamWithPlayers(id);
            var leavingUser = await userManager.GetUserAsync(HttpContext.User);

            if (teamToLeave == null || leavingUser == null)
            {
                return this.NotFound();
            }

            await this.teamService.LeaveTeamAsync(teamToLeave, leavingUser);

            return this.RedirectToAction("Details", new { id = id });
        }
    }
}