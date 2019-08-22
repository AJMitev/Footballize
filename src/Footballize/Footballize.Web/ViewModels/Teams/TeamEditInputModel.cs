namespace Footballize.Web.ViewModels.Teams
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Services.Mapping;

    public class TeamEditInputModel : IMapFrom<Team>, IMapTo<Team>
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Change Password")]
        
        public string ChangePassword { get; set; }
        
        public IFormFile Logo { get; set; }
    }
}