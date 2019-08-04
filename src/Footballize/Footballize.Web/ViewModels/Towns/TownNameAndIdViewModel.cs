namespace Footballize.Web.ViewModels.Towns
{
    using Footballize.Models;
    using Services.Mapping;

    public class TownNameAndIdViewModel : IMapFrom<Town>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}