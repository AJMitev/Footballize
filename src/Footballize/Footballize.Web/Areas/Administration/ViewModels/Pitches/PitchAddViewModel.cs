namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;

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
    }
}