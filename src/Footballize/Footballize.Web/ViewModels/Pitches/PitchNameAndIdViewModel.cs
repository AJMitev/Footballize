namespace Footballize.Web.ViewModels.Pitches
{
    using Models;
    using Services.Mapping;

    public class PitchNameAndIdViewModel : IMapFrom<Pitch>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}