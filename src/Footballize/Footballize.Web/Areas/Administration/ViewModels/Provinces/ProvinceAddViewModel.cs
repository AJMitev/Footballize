namespace Footballize.Web.Areas.Administration.ViewModels.Provinces
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Countries;
    using Models;
    using Services.Mapping;

    public class ProvinceAddViewModel : IMapFrom<Country>, IMapFrom<CountryNameAndIdViewModel>, IHaveCustomMappings
    {
        [Required]

        public string CountryId { get; set; }
        [Required]
        [MinLength(5)]
        [StringLength(75)]
        public string Name { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CountryNameAndIdViewModel, ProvinceAddViewModel>()
                .ForMember(x => x.CountryId, cfg => cfg.MapFrom(y => y.Id))
                .ForMember(x => x.Name, cfg => cfg.Ignore())
                .ForMember(x => x.CountryName, cfg => cfg.MapFrom(y => y.Name));

            configuration.CreateMap<Country, ProvinceAddViewModel>()
                .ForMember(x => x.CountryId, cfg => cfg.MapFrom(y => y.Id))
                .ForMember(x => x.CountryName, cfg => cfg.MapFrom(y => y.Name))
                .ForMember(x=>x.Name, cfg=>cfg.Ignore());
        }
    }
}