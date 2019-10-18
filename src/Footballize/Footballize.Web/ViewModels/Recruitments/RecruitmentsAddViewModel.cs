namespace Footballize.Web.ViewModels.Recruitments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class RecruitmentsAddViewModel : IMapFrom<Recruitment>
    {
        private const string StartingTimeErrorMessage = "Starting time should be in future.";
        private const int TitleMinLength = 5;
        private const int TitleMaxLength = 23;
        private const int DescriptionMinLength = 10;
        private const int DescriptionMaxLength = 300;
        private const string MaximumPlayersCount = "22";
        private const string MinimumPlayersCount = "1";
        private const string GuidRegularExpression =
            @"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?";

        [Required]
        [MaxLength(TitleMaxLength)]
        [MinLength(TitleMinLength)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [DateTimeInFutureOnly(StartingTimeErrorMessage)]
        [Display(Name = "Starting Time")]
        public DateTime StartingAt { get; set; }

        [Required]
        [Range(typeof(int), MinimumPlayersCount, MaximumPlayersCount)]
        [Display(Name = "How many players do you need?")]
        public int MaximumPlayers { get; set; }

        [Required]
        [RegularExpression(GuidRegularExpression, ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Required]
        [Display(Name = "Province")]
        [RegularExpression(GuidRegularExpression, ErrorMessage = "Select a Province")]
        public string ProvinceId { get; set; }

        [Required]
        [Display(Name = "Town")]
        [RegularExpression(GuidRegularExpression, ErrorMessage = "Select Town")]
        public string TownId { get; set; }

        [Required]
        [Display(Name = "Pitch")]
        [RegularExpression(GuidRegularExpression, ErrorMessage = "Select Pitch")]
        public string PitchId { get; set; }
    }
}