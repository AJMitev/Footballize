namespace Footballize.Services.Tests.TestViewModels
{
    using Footballize.Models;
    using Mapping;

    public class PitchTestViewModel : IMapFrom<Pitch>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}