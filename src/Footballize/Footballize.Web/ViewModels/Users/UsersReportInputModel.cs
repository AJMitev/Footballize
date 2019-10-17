namespace Footballize.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class UsersReportInputModel : IMapTo<UserReport>, IMapFrom<User>, IHaveCustomMappings
    {
        private const int ReportTypeRangeStart = 1;
        private const int ReportTypeRangeEnd = 2;
        private const int TextMinimumLength = 5;
        private const int TextMaximumLength = 350;

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        [Range(ReportTypeRangeStart, ReportTypeRangeEnd)]
        public ReportType Type { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(TextMaximumLength, MinimumLength = TextMinimumLength)]
        public string Text { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UsersReportInputModel, UserReport>()
                .ForMember(x => x.ReportedUserId, cfg => cfg.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}