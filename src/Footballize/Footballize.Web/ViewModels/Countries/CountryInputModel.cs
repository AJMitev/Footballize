using System.ComponentModel.DataAnnotations;

namespace Footballize.Web.Models.Countries
{
    public class CountryInputModel
    {
        public string Id { get; set; }
        [Required]
        [MinLength(4)]
        [StringLength(30)]
        public string Name { get; set; }
    }
}