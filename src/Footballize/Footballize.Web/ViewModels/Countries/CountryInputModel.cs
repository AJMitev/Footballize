namespace Footballize.Web.ViewModels.Countries
{
    using System.ComponentModel.DataAnnotations;
    using Footballize.Models;
    using Services.Mapping;

    public class CountryInputModel : IMapFrom<Country>
    {
        public string Id { get; set; }
        [Required]
        [MinLength(4)]
        [StringLength(30)]
        public string Name { get; set; }
    }
}