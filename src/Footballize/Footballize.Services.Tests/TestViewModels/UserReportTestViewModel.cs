namespace Footballize.Services.Tests.TestViewModels
{
    using Footballize.Models;
    using Mapping;

    public class UserReportTestViewModel : IMapFrom<UserReport>
    {
        public string Text { get; set; }
    }
}