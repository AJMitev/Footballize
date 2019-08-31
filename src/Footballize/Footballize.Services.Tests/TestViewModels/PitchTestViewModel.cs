namespace Footballize.Services.Tests.TestViewModels
{
    using Mapping;
    using Models;

    public class PitchTestViewModel : IMapFrom<Pitch>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}