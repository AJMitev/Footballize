﻿namespace Footballize.Web.ViewModels.Provinces
{
    using System.ComponentModel.DataAnnotations;
    using Footballize.Models;
    using Services.Mapping;

    public class ProvinceAddInputModel : IMapTo<Province>
    {
        [Required]
        [MinLength(5)]
        [StringLength(75)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")]
        public string CountryId { get; set; }
    }
}