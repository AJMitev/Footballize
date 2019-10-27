namespace Footballize.Services.Models.Pitch
{
    using Footballize.Models;
    using Mapping;

    public class PitchServiceModel : IMapFrom<Pitch>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}