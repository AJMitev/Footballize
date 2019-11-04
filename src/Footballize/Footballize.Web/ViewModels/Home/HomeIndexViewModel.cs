namespace Footballize.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Services.Models.Pitch;

    public class HomeIndexViewModel
    {
        public  ICollection<HomeGameViewModel> Gathers { get; set; }
        public  ICollection<HomeGameViewModel> Recruitments { get; set; }

        public ICollection<MostUsedPitchServiceModel> Pitches { get; set; }
    }
}