namespace Footballize.Services.Tests.TestViewModels
{
    using Mapping;
    using Models;

    public class UserTestViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}