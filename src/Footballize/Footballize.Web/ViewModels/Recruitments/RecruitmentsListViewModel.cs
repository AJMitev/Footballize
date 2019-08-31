namespace Footballize.Web.ViewModels.Recruitments
{
    using System.Collections.Generic;

    public class RecruitmentsListViewModel
    {
        public IEnumerable<RecruitmentIndexViewModel> Recruitments { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int RecruitmentsCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}