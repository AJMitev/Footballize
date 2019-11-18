namespace Footballize.Web.Areas.Administration.ViewModels.Countries
{
    using System.Linq;
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class CountryIndexViewModel : IMapFrom<Country>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public int ProvincesCount { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Country, CountryIndexViewModel>()
                .ForMember(x => x.ProvincesCount, o => o.MapFrom(p => p.Provinces.Count));
        }
    }
}