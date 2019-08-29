namespace Footballize.Web.Areas.Administration.ViewModels.Towns
{
    using Models;
    using Services.Mapping;

    public class TownNameAndIdViewModel : IMapFrom<Town>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}