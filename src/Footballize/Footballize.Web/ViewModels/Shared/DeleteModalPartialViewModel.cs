namespace Footballize.Web.ViewModels.Shared
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class DeleteModalPartialViewModel : IMapFrom<Gather>, IMapFrom<Recruitment>
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}