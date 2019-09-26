namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Services.Mapping;

    public class PitchEditInputModel : IMapTo<Pitch>
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

        [Required]
        public string AddressId { get; set; }
        
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(MaximumFileSize)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        public IFormFile Cover { get; set; }

        [Range(LongitudeMin, LongitudeMax)]
        [Display(Name = "Longitude")]
        public double LocationLongitude { get; set; }

         [Range(LatitudeMin, LatitudeMax)]
        [Display(Name = "Latitude")]
        public double LocationLatitude { get; set; }

    }
}