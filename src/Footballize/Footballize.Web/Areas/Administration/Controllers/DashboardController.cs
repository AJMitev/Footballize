namespace Footballize.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Dashboard;


    public class DashboardController : AdminController
    {
        private const int InactivityDays = 10;
        private const int NewbiePeriodInDays = 30;

        private readonly IRecruitmentService recruitmentService;
        private readonly IUserService userService;
        private readonly IGatherService gatherService;

        public DashboardController(IRecruitmentService recruitmentService, IUserService userService, IGatherService gatherService)
        {
            this.recruitmentService = recruitmentService;
            this.userService = userService;
            this.gatherService = gatherService;
        }

        public IActionResult Index()
        {
            var bannedPlayers = this.userService.GetAll<BannedUsersViewModel>(x => x.IsBanned);

            var indexModel = new IndexViewModel
            {
                Id = this.User.GetId(),
                RecruitmentsCount = this.recruitmentService.GetCount(),
                BannedPlayersCount = bannedPlayers.Count(),
                BannedUsers = bannedPlayers,
                RegisteredUsersCount = this.userService.GetUsersCount(),
                GathersPlayedCount = this.gatherService.GetCount(),
                ReportedUsers = this.userService.GetUserReports<ReportedUsersViewModel>()
            };

            return this.View(indexModel);
        }

        public IActionResult Users(int id)
        {
            var users = this.userService.GetAll<UserDetailsViewModel>();
            var bannedPlayersCount =  this.userService.GetAll<BannedUsersViewModel>(x => x.IsBanned)
                .Count();

            var inactiveUsersCount = this.userService.GetInactiveUsers<UserDetailsViewModel>(InactivityDays)
                .Count();

            var newUsersCount = this.userService.GetAll<UserDetailsViewModel>()
                .Count(x => (DateTime.UtcNow - x.CreatedOn).TotalDays <= NewbiePeriodInDays);
            
            id = Math.Max(1, id);
            var skip = (id - 1) * AdminUsersViewModel.ItemsPerPage;

            var filteredItems = users.Skip(skip).
                Take(AdminUsersViewModel.ItemsPerPage)
                .ToList();

            var usersCount = users.Count();
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
