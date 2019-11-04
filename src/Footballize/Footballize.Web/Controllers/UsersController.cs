namespace Footballize.Web.Controllers
{
    using System.Threading.Tasks;
    using Common;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using ViewModels.Users;

    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public UsersController(UserManager<User> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> AddPlaypal(string id)
        {

            if (!this.userService.Exists(id) || !this.userService.Exists(this.User.GetId()))
            {
                return this.NotFound();
            }

            if (id == this.User.GetId())
            {
               this.DisplayError(GlobalConstants.InvalidPlaypalErrorMessage);
            }


            await this.userService.AddPlaypal(id, this.User.GetId());



            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> RemovePlaypal(string id)
        {
            if (!this.userService.Exists(id) || !this.userService.Exists(this.User.GetId()))
            {
                return this.NotFound();
            }

            if (id == this.User.GetId())
            {
                this.DisplayError(GlobalConstants.InvalidPlaypalErrorMessage);
            }

            await this.userService.RemovePlaypal(id, this.User.GetId());

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!this.userService.Exists(id))
            {
                return this.NotFound();
            }

            var model = await this.userService.GetByIdAsync<UserDetailsViewModel>(id);

            if (string.IsNullOrEmpty(model.ProfilePicturePathToFile))
            {
                model.ProfilePicturePathToFile =
                    "https://cdn3.iconfinder.com/data/icons/avatars-15/64/_Bearded_Man-17-512.png";
            }

            model.IsSameUser = id == this.User.GetId();

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


            if (!this.userService.Exists(id))
            {
                return this.NotFound();
            }

            await this.userService.BanPlayer(id, minutes: 15);
            return this.RedirectToAction(nameof(Details), new { id });
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

            if (!this.userService.Exists(id))
            {
                return this.NotFound();
            }

            await this.userService.RemoveBan(id);
            return this.RedirectToAction(nameof(Details), new { id });
        }


        [HttpGet]
        public async Task<IActionResult> Report(string id)
        {
            if (!this.userService.Exists(id))
            {
                return this.NotFound();
            }

            var userToReport = await this.userService.GetByIdAsync<UsersReportInputModel>(id);

            return this.View(userToReport);
        }

        [HttpPost]
        public async Task<IActionResult> Report(UsersReportInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.userService.CreateReport(model.Text, model.Type, model.Id);

            return this.RedirectToAction("Index", "Home");
        }
    }
}