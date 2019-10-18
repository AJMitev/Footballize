namespace Footballize.Web.Areas.Administration.ViewModels.Dashboard
{
    using AutoMapper;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class ReportedUsersViewModel : IMapFrom<UserReport>, IMapFrom<User>, IHaveCustomMappings
    {
        public ReportType Type { get; set; }
        public string Text { get; set; }
        public string ReportedPlayerId { get; set; }
        public string ReportedPlayerName{ get; set; }
        public string ReportedPlayerUsername{ get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserReport, ReportedUsersViewModel>()
                .ForMember(x => x.ReportedPlayerId, cfg => cfg.MapFrom(y => y.ReportedUserId))
                .ForMember(x => x.ReportedPlayerUsername, cfg => cfg.MapFrom(y => y.ReportedUser.UserName))
                .ForMember(x => x.ReportedPlayerName, cfg => cfg.MapFrom(y => y.ReportedUser.FirstName + " " + y.ReportedUser.LastName));
        }
    }
}