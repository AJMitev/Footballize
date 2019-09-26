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
        private const string GuidExpression = @"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?";
        private const int TitleMinLength = 10;
        private const int TitleMaxLength = 30;
        private const int DescriptionMinLength = 10;
        private const int DescriptionMaxLength = 300;

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
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
        [RegularExpression(GuidExpression)]
        public string PitchId { get; set; }
    }
}