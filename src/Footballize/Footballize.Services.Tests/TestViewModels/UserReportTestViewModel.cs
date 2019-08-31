namespace Footballize.Services.Tests.TestViewModels
{
    using Mapping;
    using Models;

    public class UserReportTestViewModel : IMapFrom<UserReport>
    {
        public string Text { get; set; }
    }
}