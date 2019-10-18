namespace Footballize.Web.ViewModels.Users
{
    using System.Linq;
    using AutoMapper;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class UserGameDetailsViewModel : IMapFrom<User>, IMapTo<User>, IMapFrom<RecruitmentUser>, IMapFrom<GatherUser>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public int GamesCompleted { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<GatherUser, UserGameDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(y => y.User.UserName))
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.User.FirstName + " " + y.User.LastName))
                .ForMember(x => x.GamesCompleted, opt => opt.MapFrom(y => y.User.GathersPlayed.Count));

            configuration.CreateMap<RecruitmentUser, UserGameDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(y => y.User.UserName))
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.User.FirstName + " " + y.User.LastName))
                .ForMember(x => x.GamesCompleted, opt => opt.MapFrom(y => y.User.GathersPlayed.Count + y.User.GamesRecruited.Count));

            configuration.CreateMap<User, UserGameDetailsViewModel>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.FirstName + " " + y.LastName))
                .ForMember(x => x.GamesCompleted, opt => opt.MapFrom(y => y.GathersPlayed.Count(x=>x.Gather.Status == GameStatus.Finished) + y.GamesRecruited.Count(x => x.Recruitment.Status == GameStatus.Finished)));
        }
    }
}