namespace Footballize.Web.Areas.Administration.ViewModels.Dashboard
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class UserDetailsViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public int GamesRecruitedCount { get; set; }
        public int GathersPlayedCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsBanned { get; set; }
        

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserDetailsViewModel>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(y => y.FirstName + " " + y.LastName))
                .ForMember(x => x.GamesRecruitedCount, opt => opt.MapFrom(y => y.GamesRecruited.Count(x => x.Recruitment.Status == GameStatus.Finished)))
                .ForMember(x => x.GathersPlayedCount, opt => opt.MapFrom(y => y.GathersPlayed.Count(x => x.Gather.Status == GameStatus.Finished)));
        }
    }
}