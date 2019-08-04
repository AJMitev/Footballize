namespace Footballize.Web.ViewModels.Playfields
{
    using System.ComponentModel.DataAnnotations;
    using Footballize.Models;
    using Services.Mapping;

    public class PlayfiledAddInputModel : IMapTo<Playfield>,IMapTo<Address>
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Display(Name = "Town")]
        [Required(ErrorMessage = "Select Town")]
        public string TownId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Street { get; set; }

        [Required]
        [Range(typeof(int),"1","999")]
        public int Number { get; set; }
    }
}