namespace Footballize.Web.ViewModels.Users
{
    using Models;
    using Services.Mapping;

    public class UserGatherDetailsViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int GamesCompleted { get; set; }
    }
}