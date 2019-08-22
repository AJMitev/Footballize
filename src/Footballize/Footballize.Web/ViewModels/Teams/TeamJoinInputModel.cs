namespace Footballize.Web.ViewModels.Teams
{
    using System.ComponentModel.DataAnnotations;

    public class TeamJoinInputModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}