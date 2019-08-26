namespace Footballize.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class UsersReportInputModel : IMapTo<UserReport>, IMapFrom<User>, IHaveCustomMappings
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName{ get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        [Range(1,2)]
        public ReportType Type { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(350,MinimumLength = 5)]
        public string Text { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<UsersReportInputModel, UserReport>()
                .ForMember(x => x.ReportedUserId, cfg => cfg.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}