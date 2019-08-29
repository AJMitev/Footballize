namespace Footballize.Web.ViewModels.Recruitments
{
    using System;
    using Areas.Administration.ViewModels.Pitches;
    using AutoMapper;
    using Models;
    using Models.Enums;
    using Services.Mapping;
    using Users;

    public class RecruitmentIndexViewModel : IMapFrom<Recruitment>, IHaveCustomMappings
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime StartingAt { get; set; }

        public PitchIndexViewModel Pitch { get; set; }
        public GameStatus Status { get; set; }

        public string Description { get; set; }

        public int PlayersEnrolled { get; set; }

        public int MaximumPlayers { get; set; }

        public UserSimpleViewModel Creator { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Recruitment, RecruitmentIndexViewModel>()
                .ForMember(x => x.PlayersEnrolled, opt => opt.MapFrom(y => y.Players.Count));
        }
    }
}