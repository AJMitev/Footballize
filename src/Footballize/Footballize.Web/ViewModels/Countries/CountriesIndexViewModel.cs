namespace Footballize.Web.Models.Countries
{
    using Footballize.Models;
    using Services.Mapping;

    public class CountriesIndexViewModel : IMapFrom<Country>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}