namespace Footballize.Web.ViewModels.Countries
{
    using Footballize.Models;
    using Services.Mapping;

    public class CountryNameAndIdViewModel : IMapFrom<Country>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}