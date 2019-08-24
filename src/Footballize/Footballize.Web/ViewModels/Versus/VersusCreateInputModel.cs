namespace Footballize.Web.ViewModels.Versus
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Models;
    using Services.Mapping;

    public class VersusCreateInputModel : IMapTo<Versus>
    {
        private const string StartingTimeErrorMessage = "Starting time should be in future.";

        [Required]
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
        [Display(Name = "Pitch")]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string PitchId { get; set; }

        public string TeamId { get; set; }
    }
}