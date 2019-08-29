namespace Footballize.Web.Areas.Administration.ViewModels.Dashboard
{
    using System.Collections.Generic;

    public class AdminUsersViewModel
    {
        public ICollection<UserDetailsViewModel> Users { get; set; }
        public int NewUsersCount { get; set; }
        public int InactiveUsersCount { get; set; }
        public int BannedUsersCount { get; set; }
    }
}