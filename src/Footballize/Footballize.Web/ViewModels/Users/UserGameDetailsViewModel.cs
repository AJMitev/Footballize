namespace Footballize.Web.ViewModels.Users
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class UserGameDetailsViewModel : IMapFrom<RecruitmentUser>, IMapFrom<GatherUser>, IMapFrom<TeamUser>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public int GamesCompleted { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<GatherUser, UserGameDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(y => y.User.UserName))
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.User.FirstName + " " +  y.User.LastName))
                .ForMember(x => x.GamesCompleted, opt => opt.MapFrom(y => y.User.GathersPlayed.Count));

            configuration.CreateMap<RecruitmentUser, UserGameDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(y => y.User.UserName))
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.User.FirstName + " " +  y.User.LastName))
                .ForMember(x => x.GamesCompleted, opt => opt.MapFrom(y => y.User.GathersPlayed.Count + y.User.GamesRecruited.Count));

            configuration.CreateMap<TeamUser, UserGameDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(y => y.User.UserName))
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.User.FirstName + " " + y.User.LastName))
                .ForMember(x => x.GamesCompleted, opt => opt.MapFrom(y => y.User.GathersPlayed.Count + y.User.GamesRecruited.Count));
        }
    }
}