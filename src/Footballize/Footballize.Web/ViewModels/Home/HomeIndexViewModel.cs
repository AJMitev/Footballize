namespace Footballize.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Areas.Administration.ViewModels.Pitches;

    public class HomeIndexViewModel
    {
        public  ICollection<HomeGameViewModel> Gathers { get; set; }
        public  ICollection<HomeGameViewModel> Recruitments { get; set; }

        public ICollection<HomePitchViewModel> Pitches { get; set; }
    }
}