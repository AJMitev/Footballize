namespace Footballize.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
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
            var bannedPlayers = this.userService.GetBannedUsers<BannedUsersViewModel>();

            var indexModel = new IndexViewModel
            {
                Id = currentUser.Id,
                RecruitmentsCount = this.recruitmentService.GetRecruitments<RecruitmentIndexViewModel>().Count,
                BannedPlayersCount = bannedPlayers.Count,
                BannedUsers = bannedPlayers,
                RegisteredUsersCount = this.userService.GetUsersCount(),
                GathersPlayedCount = this.gatherServices.GetGatherCount(),
                ReportedUsers = this.userService.GetReportedUsers<ReportedUsersViewModel>()
            };

            return this.View(indexModel);
        }
    }
}
