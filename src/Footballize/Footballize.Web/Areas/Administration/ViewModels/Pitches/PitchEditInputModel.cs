namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Services.Mapping;

    public class PitchEditInputModel : IMapTo<Pitch>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AddressId { get; set; }
        
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5* 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        public IFormFile Cover { get; set; }

        [Range(-180.0d, 180.0d)]
        [Display(Name = "Longitude")]
        public double LocationLongitude { get; set; }

        [Range(-90.0d, 90.0d)]
        [Display(Name = "Latitude")]
        public double LocationLatitude { get; set; }

    }
}