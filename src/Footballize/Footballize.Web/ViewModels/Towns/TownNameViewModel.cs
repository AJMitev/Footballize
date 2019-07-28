namespace Footballize.Web.Models.Towns
{
    using Footballize.Models;
    using Services.Mapping;

    public class TownNameViewModel : IMapFrom<Town>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}