namespace Footballize.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class UserDetailsViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsBanned { get; set; }
        public string Email { get; set; }
        public bool IsSameUser { get; set; }
        public ICollection<GatherUser> GathersPlayed { get; set; }
        public ICollection<RecruitmentUser> GamesRecruited { get; set; }
        public ICollection<Playpal> PlaypalsAdded { get; set; }
        public ICollection<Playpal> PlaypalsAddedMe { get; set; }
    }
}