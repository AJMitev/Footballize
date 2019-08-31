namespace Footballize.Web.Areas.Administration.ViewModels.Dashboard
{
    using System.Collections.Generic;
    using Abstractions;

    public class AdminUsersViewModel : PaginationViewModel<UserDetailsViewModel>
    {
        public const int ItemsPerPage = 10;

        public int NewUsersCount { get; set; }
        public int InactiveUsersCount { get; set; }
        public int BannedUsersCount { get; set; }
    }
}