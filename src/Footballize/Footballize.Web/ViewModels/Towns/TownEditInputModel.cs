﻿namespace Footballize.Web.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;
    using Footballize.Models;
    using Services.Mapping;

    public class TownEditInputModel : IMapTo<Town>
    {
        public string Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Select Province")]
        public string ProvinceId { get; set; }
    }
}