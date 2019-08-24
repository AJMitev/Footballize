namespace Footballize.Web.ViewModels.Shared
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class DeleteModalPartialViewModel : IMapFrom<Gather>, IMapFrom<Team>, IMapFrom<Recruitment>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Team, DeleteModalPartialViewModel>()
                .ForMember(x => x.Title, cfg => cfg.MapFrom(y => y.Name));
        }
    }
}