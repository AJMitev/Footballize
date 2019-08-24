namespace Footballize.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Footballize.Services.Exceptions;
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

            try
            {
                await this.teamService.CreateTeamAsync(newTeam);
                return this.RedirectToAction("Index");
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var teamToEdit = this.teamService.GetTeam<TeamEditInputModel>(id);

            if (teamToEdit == null)
            {
                return this.NotFound();
            }

            return this.View(teamToEdit);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(TeamEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var teamToEdit = await this.teamService.GetTeamAsync(model.Id);

                Mapper.Map<TeamEditInputModel, Team>(model,teamToEdit);

                if (!string.IsNullOrEmpty(model.ChangePassword) && !string.IsNullOrWhiteSpace(model.ChangePassword))
                {
                    await this.teamService.UpdateTeamPassword(teamToEdit, model.ChangePassword);
                }

                await this.teamService.UpdateTeam(teamToEdit);

                return this.RedirectToAction("Details", new {id = model.Id});
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id = model.Id });
            }
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
        public async Task<IActionResult> Kick(string teamId, string playerId)
        {
            try
            {
                var team = await this.teamService.GetTeamAsync(teamId);
                var currentUser = await this.userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return this.NotFound();
                }

                if (team.Owner != currentUser)
                {
                    return this.Forbid();
                }

                await this.teamService.LeaveTeamAsync(team, playerId);
                return this.RedirectToAction("Details", new { id = teamId });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id = teamId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var team =  await this.teamService.GetTeamAsync(id);

            if (team == null)
            {
                return this.NotFound();
            }

            try
            {
                await this.teamService.DeleteTeamAsync(team);

                return this.RedirectToAction("Index");
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Join(TeamJoinInputModel model)
        {
            try
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
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id = model.Id });
            }
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

            try
            {
                await this.teamService.LeaveTeamAsync(teamToLeave, leavingUser.Id);

                return this.RedirectToAction("Details", new { id = id });
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Details", new { id = id });
            }
        }
    }
}