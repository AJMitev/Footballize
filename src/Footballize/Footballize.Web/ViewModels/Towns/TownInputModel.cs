namespace Footballize.Web.Models.Towns
{
    using System.ComponentModel.DataAnnotations;

    public class TownInputModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Select Country")]
        public string CountryId { get; set; }
        [Required(ErrorMessage = "Select Province")]
        public string ProvinceId { get; set; }
    }
}