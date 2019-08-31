namespace Footballize.Web.Areas.Administration.ViewModels.Dashboard
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public string Id { get; set; }
        public int GathersPlayedCount { get; set; }
        public int RecruitmentsCount { get; set; }
        public int BannedPlayersCount { get; set; }
        public int RegisteredUsersCount { get; set; }
        public IEnumerable<ReportedUsersViewModel> ReportedUsers { get; set; }
        public IEnumerable<BannedUsersViewModel> BannedUsers { get; set; }
    }
}
