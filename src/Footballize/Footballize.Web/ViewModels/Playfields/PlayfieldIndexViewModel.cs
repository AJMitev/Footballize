namespace Footballize.Web.ViewModels.Playfields
{
    using AutoMapper;
    using Footballize.Models;
    using Services.Mapping;

    public class PlayfieldIndexViewModel : IMapFrom<Playfield>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string TownName { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Playfield, PlayfieldIndexViewModel>()
                .ForMember(x => x.CountryName, opt => opt.MapFrom(y => y.Address.Town.Province.Country.Name))
                .ForMember(x => x.TownName, opt => opt.MapFrom(y => y.Address.Town.Name));
        }
    }
}