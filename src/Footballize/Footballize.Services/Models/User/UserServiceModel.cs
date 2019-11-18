namespace Footballize.Services.Models.User
{
    using AutoMapper;
    using Footballize.Models;
    using Mapping;

    public class UserServiceModel : IMapFrom<User>, IMapFrom<RecruitmentUser>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RecruitmentUser, UserServiceModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(y => y.UserId))
                .ForMember(x => x.FirstName, cfg => cfg.MapFrom(y => y.User.FirstName))
                .ForMember(x => x.LastName, cfg => cfg.MapFrom(y => y.User.LastName))
                .ForMember(x => x.UserName, cfg => cfg.MapFrom(y => y.User.UserName));
        }
    }
}