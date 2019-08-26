namespace Footballize.Web.ViewModels.Users
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class UserPlaypalViewModel : IMapFrom<Playpal>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GamesRecruited { get; set; }
        public int GathersPlayed { get; set; }

    }
}