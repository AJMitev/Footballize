namespace Footballize.Web.ViewModels.Gathers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Models.Enums;

    public class GatherAddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Starting Time")]
        public DateTime StartingAt { get; set; }

        [Required]
        [Range(typeof(int), "1", "4")]
        [Display(Name = "Team Format")]
        public TeamFormat TeamFormat { get; set; }

        [Required]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Required]
        [Display(Name = "Province")]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string ProvinceId { get; set; }

        [Required]
        [Display(Name = "Town")]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string TownId { get; set; }

        [Required]
        [Display(Name = "Pitch")]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string PitchId { get; set; }
    }
}