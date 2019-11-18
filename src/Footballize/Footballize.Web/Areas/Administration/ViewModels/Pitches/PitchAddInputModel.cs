namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Services.Mapping;

    public class PitchAddInputModel : IMapTo<Pitch>, IMapTo<Address>, IMapTo<Location>
    {
        private const int MaximumFileSize = 5 * 1024 * 1024;
        private const int NameMinLength = 5;
        private const int StreetMinLength = 3;
        private const int StreetMaxLength = 300;
        private const string StreetMinNumber = "1";
        private const string StreetMaxNumber = "999";
        private const double LongitudeMin = -180.0d;
        private const double LongitudeMax = 180.0d;
        private const double LatitudeMin = -90.0d;
        private const double LatitudeMax = 90.0d;

        [Required]
        [MinLength(NameMinLength)] 
        public string Name { get; set; }

        [Display(Name = "Town")]
        [Required(ErrorMessage = "Select Town")]
        public string TownId { get; set; }

        [Required]
        [MinLength(StreetMinLength)]
        [MaxLength(StreetMaxLength)]
        public string Street { get; set; }

        [Required]
        [Range(typeof(int), StreetMinNumber, StreetMaxNumber)]
        public int Number { get; set; }


        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(MaximumFileSize)]
        [AllowedExtensions("jpg", "png")]
        public IFormFile Cover { get; set; }


        [Range(LongitudeMin, LongitudeMax)]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Range(LatitudeMin, LatitudeMax)]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
    }
}