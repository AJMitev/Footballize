namespace Footballize.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services.Data;
    using Services.Exceptions;
    using ViewModels.Users;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public UsersController(UserManager<User> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var players = this.userService.GetUsers<UserGameDetailsViewModel>();

            return this.View(players);
        }

        [HttpGet]
        public async Task<IActionResult> AddPlaypal(string id)
        {
            var playpal = this.userService.GetUser(id);

            if (playpal == null)
            {
                return this.NotFound();
            }

            var httpUser = await this.userManager.GetUserAsync(User);
            var currentUser = this.userService.GetUser(httpUser.Id);

            try
            {
                await this.userService.AddPlaypal(playpal, currentUser);
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
            }

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> RemovePlaypal(string id)
        {
            var playpal = await this.userService.GetUserAsync(id);

            if (playpal == null)
            {
                return this.NotFound();
            }

            var currentUser = await this.userManager.GetUserAsync(User);

            try
            {
                await this.userService.RemovePlaypal(playpal, currentUser);
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
            }

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = this.userService.GetUser(id);

            if (user == null)
            {
                return this.NotFound();
            }

            var model = Mapper.Map<UserDetailsViewModel>(user);
            var currentUser = await userManager.GetUserAsync(User);
            model.IsSameUser = id == currentUser.Id;

            return View(model);
        }
    }
}