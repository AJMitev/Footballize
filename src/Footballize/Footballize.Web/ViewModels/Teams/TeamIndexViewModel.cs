namespace Footballize.Web.ViewModels.Teams
{
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class TeamIndexViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public int NumberOfPlayers { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Team, TeamIndexViewModel>()
                .ForMember(x => x.Nationality, cfg => cfg.MapFrom(y => y.Nationality.Name))
                .ForMember(x => x.NumberOfPlayers, cfg => cfg.MapFrom(y => y.Players.Count));
        }
    }
}