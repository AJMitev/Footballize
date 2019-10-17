namespace Footballize.Web.Areas.Administration.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class TownEditViewModel : IMapFrom<Town>, IHaveCustomMappings
    {
        private const int NameMinLength = 5;

        public string Id { get; set; }
        [Required]
        [MinLength(NameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Select Province")]
        public string ProvinceId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Town, TownEditViewModel>()
                .ForMember(x => x.CountryId, opt => opt.MapFrom(y => y.Province.CountryId));
        }
    }
}