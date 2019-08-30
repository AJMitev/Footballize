namespace Footballize.Web.ViewModels.Home
{
    using Services.DTOs;
    using Services.Mapping;

    public class HomePitchViewModel : IMapFrom<MostUsedPitchDTO>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TimesUsed { get; set; }
        public string Location { get; set; }
    }
}