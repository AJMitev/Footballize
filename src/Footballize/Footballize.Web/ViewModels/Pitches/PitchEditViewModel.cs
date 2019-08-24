namespace Footballize.Web.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class PitchEditViewModel : IMapFrom<Pitch>, IMapFrom<PitchEditInputModel>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "Town")]

        public string TownName { get; set; }
        [Display(Name = "Province")]
        public string ProvinceName { get; set; }

        public Address Address { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Pitch, PitchEditViewModel>()
                .ForMember(x => x.CountryName, opt => opt.MapFrom(y => y.Address.Town.Province.Country.Name))
                .ForMember(x => x.ProvinceName, opt => opt.MapFrom(y => y.Address.Town.Province.Name))
                .ForMember(x => x.TownName, opt => opt.MapFrom(y => y.Address.Town.Name));
        }
    }
}