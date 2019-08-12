namespace Footballize.Web.ViewModels.Towns
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class TownWithProvinceViewModel : IMapFrom<Town>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Town, TownWithProvinceViewModel>()
                .ForMember(x => x.Name,
                    cfg => cfg.MapFrom(y => string.Concat(y.Name, ", ", y.Province.Name)));
        }
    }
}