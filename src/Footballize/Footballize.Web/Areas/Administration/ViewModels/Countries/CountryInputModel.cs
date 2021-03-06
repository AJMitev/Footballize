﻿namespace Footballize.Web.Areas.Administration.ViewModels.Countries
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class CountryInputModel : IMapFrom<Country>, IMapTo<Country>
    {
        public string Id { get; set; }
        [Required]
        [MinLength(5)]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        [StringLength(5)]
        [Display(Name = "ISO Code")]
        public string IsoCode { get; set; }
    }
}