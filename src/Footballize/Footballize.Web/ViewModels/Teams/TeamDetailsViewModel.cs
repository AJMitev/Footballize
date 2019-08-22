namespace Footballize.Web.ViewModels.Teams
{
    using System.Collections.Generic;
    using Models;
    using Services.Mapping;
    using Users;

    public class TeamDetailsViewModel : IMapFrom<Team>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Country Nationality { get; set; }
        public User Owner { get; set; }
        public bool IsBanned { get; set; }
        public ICollection<UserGameDetailsViewModel> Players { get; set; }
        //TODO: Add Matches
    }
}