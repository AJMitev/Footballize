namespace Footballize.Web.ViewModels.Home
{
    using Services.Mapping;
    using Services.Models.Pitch;

    public class HomePitchViewModel : IMapFrom<MostUsedPitchServiceModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TimesUsed { get; set; }
        public string Location { get; set; }
    }
}