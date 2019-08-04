namespace Footballize.Web.ViewModels.Pitches
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using Services.Mapping;

    public class PitchEditInputModel : IMapTo<Pitch>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AddressId { get; set; }
    }
}