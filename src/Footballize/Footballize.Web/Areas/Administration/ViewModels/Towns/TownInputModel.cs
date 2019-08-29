namespace Footballize.Web.Areas.Administration.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class TownInputModel : IMapTo<Town>
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Select Province")]
        public string ProvinceId { get; set; }
    }
}