﻿namespace Footballize.Web.ViewModels.Gathers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Models.Enums;
    using Services.Mapping;

    public class GatherAddViewModel: IMapFrom<GatherAddInputModel>
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
        [Range(typeof(int), "1", "4")]
        [Display(Name = "Team Format")]
        public TeamFormat TeamFormat { get; set; }

        [Required]
        [RegularExpression(GuidExpression,ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Required]
        [Display(Name = "Province")]
        [RegularExpression(GuidExpression, ErrorMessage = "Select a Province")]
        public string ProvinceId { get; set; }

        [Required]
        [Display(Name = "Town")]
        [RegularExpression(GuidExpression, ErrorMessage = "Select Town")]
        public string TownId { get; set; }

        [Required]
        [Display(Name = "Pitch")]
        [RegularExpression(GuidExpression, ErrorMessage = "Select Pitch")]
        public string PitchId { get; set; }
    }
}