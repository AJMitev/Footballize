namespace Footballize.Services.Models.Pitch
{
    using Footballize.Models;
    using Mapping;

    public class MostUsedPitchServiceModel : IMapFrom<Pitch>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TimesUsed { get; set; }
        public string Location { get; set; }
    }
}