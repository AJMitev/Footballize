namespace Footballize.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using AutoMapper;
    using Gathers;
    using Models;
    using Recruitments;
    using Services.Mapping;

    public class UserDetailsViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsBanned { get; set; }
        public string Email { get; set; }
        public bool IsSameUser { get; set; }
        public ICollection<GatherUserViewModel> GathersPlayed { get; set; }
        public ICollection<RecruitmentUserViewModel> GamesRecruited { get; set; }
        public ICollection<PlaypalViewModel> PlaypalsAdded { get; set; }
        public ICollection<PlaypalViewModel> PlaypalsAddedMe { get; set; }
        public string ProfilePicturePathToFile { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserDetailsViewModel>()
                .ForMember(x => x.ProfilePicturePathToFile, cfg => cfg.MapFrom(y => y.ProfilePicture.PathToFile));
        }
    }
}