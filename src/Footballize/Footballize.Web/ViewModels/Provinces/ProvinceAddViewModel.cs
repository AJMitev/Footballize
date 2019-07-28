namespace Footballize.Web.ViewModels.Provinces
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Countries;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProvinceAddViewModel
    {
        public SelectList Countries { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Please select valid country from the dropdown menu.")]
        public string SelectedCountryId { get; set; }
        [Required]
        [MinLength(5)]
        [StringLength(75)]
        public string Name { get; set; }
    }
}