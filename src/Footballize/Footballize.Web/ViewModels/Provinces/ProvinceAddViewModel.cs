namespace Footballize.Web.ViewModels.Provinces
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Countries;
    using Footballize.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Mapping;

    public class ProvinceAddViewModel : IMapFrom<Province>
    {
        public SelectList Countries { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string CountryId { get; set; }
        [Required]
        [MinLength(5)]
        [StringLength(75)]
        public string Name { get; set; }
    }
}