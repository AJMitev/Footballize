namespace Footballize.Web.ViewModels.Gathers
{
    using System;
    using AutoMapper;
    using Models;
    using Pitches;
    using Services.Mapping;
    using Users;

    public class GatherIndexViewModel : IMapFrom<Gather>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime StartingAt { get; set; }

        public PitchIndexViewModel Pitch { get; set; }
        
        public string Description { get; set; }

        public int PlayersEnrolled { get; set; }

        public UserSimpleViewModel Creator { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Gather, GatherIndexViewModel>()
                .ForMember(x => x.PlayersEnrolled, opt => opt.MapFrom(y => y.Players.Length));
        }
    }
}