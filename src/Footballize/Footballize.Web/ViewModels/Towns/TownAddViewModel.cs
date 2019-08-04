﻿namespace Footballize.Web.Models.Towns
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Footballize.Models;
    using Services.Mapping;

    public class TownAddViewModel : IMapFrom<Town>
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
    }
}