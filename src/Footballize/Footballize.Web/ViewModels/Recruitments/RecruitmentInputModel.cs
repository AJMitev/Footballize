namespace Footballize.Web.ViewModels.Recruitments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Models;
    using Services.Mapping;

    public class RecruitmentInputModel : IMapTo<Recruitment>
    {
        private const string StartingTimeErrorMessage = "Starting time should be in future.";
        private const string GuidExpression = @"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?";
        private const int TitleMaxLength = 23;

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [DateTimeInFutureOnly(StartingTimeErrorMessage)]
        [Display(Name = "Starting Time")]
        public DateTime StartingAt { get; set; }

        [Required]
        [Display(Name = "Players")]
        public int MaximumPlayers { get; set; }


        [Required]
        [Display(Name = "Pitch")]
        [RegularExpression(GuidExpression)]
        public string PitchId { get; set; }
    }
}