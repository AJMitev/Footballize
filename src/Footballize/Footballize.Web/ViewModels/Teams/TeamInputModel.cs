namespace Footballize.Web.ViewModels.Teams
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class TeamInputModel : IMapTo<Team>
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string CountryId { get; set; }
    }
}