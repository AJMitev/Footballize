namespace Footballize.Web.ViewModels.Users
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class UserGatherDetailsViewModel : IMapFrom<GatherUser>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public int GamesCompleted { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<GatherUser, UserGatherDetailsViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(y => y.User.UserName))
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.User.FirstName + " " +  y.User.LastName))
                .ForMember(x => x.GamesCompleted, opt => opt.MapFrom(y => y.User.GamesPlayed.Count));
        }
    }
}