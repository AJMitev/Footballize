namespace Footballize.Web.ViewModels.Recruitments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class RecruitmentInputModel : IMapTo<Recruitment>
    {
        [Required]
        [MaxLength(23)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Starting Time")]
        public DateTime StartingAt { get; set; }

        [Required]
        [Display(Name = "Players")]
        public int MaximumPlayers { get; set; }


        [Required]
        [Display(Name = "Pitch")]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string PitchId { get; set; }
    }
}