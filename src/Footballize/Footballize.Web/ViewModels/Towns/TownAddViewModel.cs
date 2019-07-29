namespace Footballize.Web.Models.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Mapping;
    using ViewModels.Countries;
    using ViewModels.Provinces;

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

        public SelectList Countries { get; set; }

        public SelectList Provinces { get; set; }
    }
}