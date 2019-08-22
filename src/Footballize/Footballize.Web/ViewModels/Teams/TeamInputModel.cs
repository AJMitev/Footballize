namespace Footballize.Web.ViewModels.Teams
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Services.Mapping;

    public class TeamInputModel : IMapTo<Team>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string CountryId { get; set; }

        public IFormFile Logo { get; set; }
    }
}