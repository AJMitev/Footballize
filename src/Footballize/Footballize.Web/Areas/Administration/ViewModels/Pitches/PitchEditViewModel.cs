namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Services.Mapping;

    public class PitchEditViewModel : IMapFrom<Pitch>, IHaveCustomMappings
    {
        private const int MaximumFileSize = 5 * 1024 * 1024;
        private const double LongitudeMin = -180.0d;
        private const double LongitudeMax = 180.0d;
        private const double LatitudeMin = -90.0d;
        private const double LatitudeMax = 90.0d;

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "Town")]

        public string TownName { get; set; }
        [Display(Name = "Province")]
        public string ProvinceName { get; set; }

        public Address Address { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(MaximumFileSize)]
        [AllowedExtensions("jpg", "png")]
        public IFormFile Cover { get; set; }

        [Range(LongitudeMin, LongitudeMax)]
        [Display(Name = "Longitude")]
        public double LocationLongitude { get; set; }

        [Range(LatitudeMin, LatitudeMax)]
        [Display(Name = "Latitude")]
        public double LocationLatitude { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pitch, PitchEditViewModel>()
                .ForMember(x => x.CountryName, opt => opt.MapFrom(y => y.Address.Town.Province.Country.Name))
                .ForMember(x => x.ProvinceName, opt => opt.MapFrom(y => y.Address.Town.Province.Name))
                .ForMember(x => x.TownName, opt => opt.MapFrom(y => y.Address.Town.Name))
                .ForMember(x => x.LocationLatitude, opt => opt.MapFrom(y => y.Address.Location.Latitude))
                .ForMember(x => x.LocationLongitude, opt => opt.MapFrom(y => y.Address.Location.Longitude));
        }
    }
}