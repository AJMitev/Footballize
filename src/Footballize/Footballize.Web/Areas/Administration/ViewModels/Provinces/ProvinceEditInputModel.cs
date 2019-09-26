namespace Footballize.Web.Areas.Administration.ViewModels.Provinces
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class ProvinceEditInputModel : IMapTo<Province>
    {
        private const string CountryExpression = @"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?";
        private const int NameMinLength = 5;
        private const int NameMaxLength = 75;

        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(CountryExpression)]
        public string CountryId { get; set; }
    }
}