namespace Footballize.Web.Areas.Administration.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class TownAddViewModel : IMapFrom<Town>
    {
        private const int NameMinLength = 5;

        [Required]
        [MinLength(NameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Select Province")]
        public string ProvinceId { get; set; }
    }
}