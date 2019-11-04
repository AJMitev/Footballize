namespace Footballize.Web.ViewModels.Users
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class PlaypalViewModel : IMapFrom<Playpal>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Playpal, PlaypalViewModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(y => y.FromUser.Id ?? y.ToUser.Id))
                .ForMember(x => x.FirstName, cfg => cfg.MapFrom(y => y.FromUser.FirstName ?? y.ToUser.FirstName))
                .ForMember(x => x.LastName, cfg => cfg.MapFrom(y => y.FromUser.LastName ?? y.ToUser.LastName))
                .ForMember(x => x.UserName, cfg => cfg.MapFrom(y => y.FromUser.UserName ?? y.ToUser.UserName));
        }
    }
}