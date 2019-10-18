namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using Services.Exceptions;
    using ViewModels.Users;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(UserManager<User> userManager, IUserService userService, IMapper mapper)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.mapper = mapper;
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

            return this.RedirectToAction("Details", new { id });
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
                await this.userService.RemovePlaypal(playpal.Id, currentUser.Id);
            }
            catch (ServiceException e)
            {
                this.TempData["Error"] = e.Message;
            }

            return this.RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = this.userService.GetUser(id);

            if (user == null)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<UserDetailsViewModel>(user);

            if (string.IsNullOrEmpty(model.ProfilePicturePathToFile))
            {
                model.ProfilePicturePathToFile =
                    "https://cdn3.iconfinder.com/data/icons/avatars-15/64/_Bearded_Man-17-512.png";
            }

            var currentUser = await userManager.GetUserAsync(User);
            model.IsSameUser = id == currentUser.Id;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Ban(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);
            var isInRole = await this.userManager.IsInRoleAsync(currentUser, GlobalConstants.CanBanPlayers);

            if (!isInRole)
            {
                return this.Unauthorized();
            }

            var player = await this.userService.GetUserAsync(id);

            if (player == null)
            {
                return this.NotFound();
            }


            await this.userService.BanPlayer(player, minutes: 15);
            return this.RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveBan(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);
            var isInRole = await this.userManager.IsInRoleAsync(currentUser, GlobalConstants.CanBanPlayers);

            if (!isInRole)
            {
                return this.Unauthorized();
            }

            var player = await this.userService.GetUserAsync(id);

            if (player == null)
            {
                return this.NotFound();
            }


            await this.userService.RemoveBan(player);
            return this.RedirectToAction("Details", new { id });
        }


        [HttpGet]
        public async Task<IActionResult> Report(string id)
        {
            var userToReport = await this.userService.GetUserAsync(id);

            if (userToReport == null)
            {
                return this.NotFound();
            }

            return this.View(this.mapper.Map<UsersReportInputModel>(userToReport));
        }

        [HttpPost]
        public async Task<IActionResult> Report(UsersReportInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var report = this.mapper.Map<UserReport>(model);

            await this.userService.CreateReport(report);

            return this.RedirectToAction("Index", "Home");
        }
    }
}