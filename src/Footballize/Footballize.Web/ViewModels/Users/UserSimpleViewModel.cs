namespace Footballize.Web.ViewModels.Users
{
    using Models;
    using Services.Mapping;

    public class UserSimpleViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }

    }
}