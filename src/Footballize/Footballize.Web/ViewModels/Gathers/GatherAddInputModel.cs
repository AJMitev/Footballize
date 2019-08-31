namespace Footballize.Web.ViewModels.Gathers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class GatherAddInputModel : IMapTo<Gather>
    {
        private const string StartingTimeErrorMessage = "Starting time should be in future.";

        [Required]
        [MinLength(10)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [DateTimeInFutureOnly(StartingTimeErrorMessage)]
        [Display(Name = "Starting Time")]
        public DateTime StartingAt { get; set; }

        [Required]
        [Display(Name = "Team Format")]
        public TeamFormat TeamFormat { get; set; }
        
        [Required]
        [Display(Name = "Pitch")]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string PitchId { get; set; }
    }
}