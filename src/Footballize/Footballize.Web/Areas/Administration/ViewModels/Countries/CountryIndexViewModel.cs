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
        public int TownsCount { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Country,CountryIndexViewModel>()
                .ForMember(x=>x.ProvincesCount,o=>o.MapFrom(p=>p.Provinces.Count))
                .ForMember(x=>x.TownsCount,o=>o.MapFrom(p=>p.Provinces.Sum(t=>t.Towns.Count)));
        }
    }
}