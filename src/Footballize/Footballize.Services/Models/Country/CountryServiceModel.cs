namespace Footballize.Services.Models.Country
{
    using Footballize.Models;
    using Mapping;

    public class CountryServiceModel : IMapFrom<Country>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
    }
}