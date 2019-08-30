namespace Footballize.Web.ViewModels.Gathers
{
    using System.Collections.Generic;

    public class GatherListViewModel
    {
        public IEnumerable<GatherIndexViewModel> Gathers { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int GathersCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}