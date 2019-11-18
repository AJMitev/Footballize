namespace Footballize.Services.Tests.TestViewModels
{
    using Footballize.Models;
    using Mapping;

    public class UserTestViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}