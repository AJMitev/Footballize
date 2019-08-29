namespace Footballize.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;
    using ViewModels.Dashboard;
    using Web.ViewModels.Recruitments;

   
    public class DashboardController : AdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IRecruitmentService recruitmentService;
        private readonly IUserService userService;
        private readonly IGatherServices gatherServices;

        public DashboardController(UserManager<User> userManager, IRecruitmentService recruitmentService, IUserService userService, IGatherServices gatherServices)
        {
            this.userManager = userManager;
            this.recruitmentService = recruitmentService;
            this.userService = userService;
            this.gatherServices = gatherServices;
        }

        public async Task<IActionResult> Index()
        {

            var currentUser = await this.userManager.GetUserAsync(User);
            var bannedPlayers = this.userService.GetUsers<BannedUsersViewModel>(x => x.IsBanned);

            var indexModel = new IndexViewModel
            {
                Id = currentUser.Id,
                RecruitmentsCount = this.recruitmentService.GetRecruitments<RecruitmentIndexViewModel>().Count,
                BannedPlayersCount = bannedPlayers.Count,
                BannedUsers = bannedPlayers,
                RegisteredUsersCount = this.userService.GetUsersCount(),
                GathersPlayedCount = this.gatherServices.GetGatherCount(),
                ReportedUsers = this.userService.GetUserReports<ReportedUsersViewModel>()
            };

            return this.View(indexModel);
        }

        public IActionResult Users()
        {
            var users = this.userService.GetUsers<UserDetailsViewModel>();
            var bannedPlayersCount = this.userService.GetUsers<BannedUsersViewModel>(x => x.IsBanned).Count;
            var inactiveUsersCount = this.userService.GetUsers<UserDetailsViewModel>(
                x => x.GathersPlayed.Any(y => (DateTime.UtcNow - y.CreatedOn).TotalDays >= 10) 
                && x.GamesRecruited.Any(y => (DateTime.UtcNow - y.CreatedOn).TotalDays >= 10)).Count;

            var newUsersCount =
                this.userService.GetUsers<UserDetailsViewModel>(x => (DateTime.UtcNow - x.CreatedOn).TotalDays >= 10).Count;

            var model = new AdminUsersViewModel
            {
                Users = users,
                InactiveUsersCount = inactiveUsersCount,
                BannedUsersCount = bannedPlayersCount,
                NewUsersCount = newUsersCount
            };

            return this.View(model);
        }
    }
}
