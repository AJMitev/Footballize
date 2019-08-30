namespace Footballize.Web.ViewModels.Home
{
    using System;
    using AutoMapper;
    using Models;
    using Services.Mapping;

    public class HomeGameViewModel : IMapFrom<Gather>, IMapFrom<Recruitment>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public DateTime StartingAt { get; set; }
        public string TownName { get; set; }
        public int PlayersEnrolledCount { get; set; }
        public int PlayerSpotsCount { get; set; }
        public string CreatorUserName { get; set; }
        public string CreatorId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Gather, HomeGameViewModel>()
                .ForMember(x => x.TownName, cfg => cfg.MapFrom(y => y.Pitch.Address.Town.Name))
                .ForMember(x => x.PlayersEnrolledCount, cfg => cfg.MapFrom(y => y.Players.Count))
                .ForMember(x => x.PlayerSpotsCount, cfg => cfg.MapFrom(y => y.MaximumPlayers));

            configuration.CreateMap<Recruitment, HomeGameViewModel>()
                .ForMember(x => x.TownName, cfg => cfg.MapFrom(y => y.Pitch.Address.Town.Name))
                .ForMember(x => x.PlayersEnrolledCount, cfg => cfg.MapFrom(y => y.Players.Count))
                .ForMember(x => x.PlayerSpotsCount, cfg => cfg.MapFrom(y => y.MaximumPlayers));
        }
    }
}