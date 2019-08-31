namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Services.Mapping;

    public class PitchAddInputModel : IMapTo<Pitch>, IMapTo<Address>, IMapTo<Location>
    {
        [Required] [MinLength(5)] public string Name { get; set; }

        [Display(Name = "Town")]
        [Required(ErrorMessage = "Select Town")]
        public string TownId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Street { get; set; }

        [Required]
        [Range(typeof(int), "1", "999")]
        public int Number { get; set; }


        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        public IFormFile Cover { get; set; }


        [Range(-180.0d, 180.0d)]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Range(-90.0d, 90.0d)]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
    }
}