namespace Footballize.Services.Models.User
{
    using Footballize.Models;
    using Mapping;

    public class UserServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}