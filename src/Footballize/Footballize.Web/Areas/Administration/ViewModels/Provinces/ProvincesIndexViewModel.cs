namespace Footballize.Web.Areas.Administration.ViewModels.Provinces
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class ProvincesIndexViewModel : IMapFrom<Province>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public int TownsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Province, ProvincesIndexViewModel>()
                .ForMember(x => x.TownsCount, o => o.MapFrom(p => p.Towns.Count));
        }
    }
}
