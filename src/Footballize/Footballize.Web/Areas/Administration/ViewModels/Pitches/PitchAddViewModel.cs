namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Microsoft.AspNetCore.Http;

    public class PitchAddViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Select Province")]
        public string ProvinceId { get; set; }

        [Display(Name = "Town")]
        [Required(ErrorMessage = "Select Town")]
        public string TownId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Street { get; set; }

        [Required]
        [MaxLength(3)]
        public int Number { get; set; }

        [Range(-180.0d, 180.0d)]
        public double Longitude { get; set; }

        [Range(-90.0d, 90.0d)]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        //[AllowedExtensions(new[] { ".jpg", ".png" })]
        public IFormFile Cover { get; set; }
    }
}