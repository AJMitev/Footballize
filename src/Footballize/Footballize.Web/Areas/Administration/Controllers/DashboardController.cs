namespace Footballize.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
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
        private readonly IGatherService gatherService;

        public DashboardController(UserManager<User> userManager, IRecruitmentService recruitmentService, IUserService userService, IGatherService gatherService)
        {
            this.userManager = userManager;
            this.recruitmentService = recruitmentService;
            this.userService = userService;
            this.gatherService = gatherService;
        }

        public async Task<IActionResult> Index()
        {

            var currentUser = await this.userManager.GetUserAsync(User);
            var bannedPlayers = this.userService.GetUsers<BannedUsersViewModel>(x => x.IsBanned);

            var indexModel = new IndexViewModel
            {
                Id = currentUser.Id,
                RecruitmentsCount = this.recruitmentService.GetAll<RecruitmentIndexViewModel>().Count,
                BannedPlayersCount = bannedPlayers.Count,
                BannedUsers = bannedPlayers,
                RegisteredUsersCount = this.userService.GetUsersCount(),
                GathersPlayedCount = this.gatherService.GetCount(),
                ReportedUsers = this.userService.GetUserReports<ReportedUsersViewModel>()
            };

            return this.View(indexModel);
        }

        public IActionResult Users(int id)
        {
            var users = this.userService.GetUsers<UserDetailsViewModel>();
            var bannedPlayersCount = this.userService.GetUsers<BannedUsersViewModel>(x => x.IsBanned).Count;
            var inactiveUsersCount = this.userService.GetUsers<UserDetailsViewModel>(
                x => x.GathersPlayed.Any(y => (DateTime.UtcNow - y.CreatedOn).TotalDays >= 10) 
                && x.GamesRecruited.Any(y => (DateTime.UtcNow - y.CreatedOn).TotalDays >= 10)).Count;

            var newUsersCount =
                this.userService.GetUsers<UserDetailsViewModel>(x => (DateTime.UtcNow - x.CreatedOn).TotalDays <= 30).Count;


            
            id = Math.Max(1, id);
            var skip = (id - 1) * AdminUsersViewModel.ItemsPerPage;

            var filteredItems = users.Skip(skip).Take(AdminUsersViewModel.ItemsPerPage).ToList();

            var usersCount = users.Count;
            var pagesCount = (int)Math.Ceiling(usersCount / (decimal)AdminUsersViewModel.ItemsPerPage);


            var model = new AdminUsersViewModel
            {
                Items = filteredItems,
                InactiveUsersCount = inactiveUsersCount,
                BannedUsersCount = bannedPlayersCount,
                NewUsersCount = newUsersCount,
                CurrentPage = id,
                PagesCount = pagesCount,
                ItemsCount = usersCount
            };

            return this.View(model);
        }
    }
}
