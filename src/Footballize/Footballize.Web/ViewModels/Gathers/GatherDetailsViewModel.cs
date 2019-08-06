namespace Footballize.Web.ViewModels.Gathers
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Enums;
    using Pitches;
    using Services.Mapping;

    public class GatherDetailsViewModel : IMapFrom<Gather>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        public PitchIndexViewModel Pitch { get; set; }
        public string PitchId { get; set; }
        public TeamFormat TeamFormat { get; set; }
        public GameStatus Status { get; set; }
        public  User Creator { get; set; }
        public int MaximumPlayersAllowed { get; set; }
        //TODO: Change it with ViewModel.
        public ICollection<GatherUser> Players { get; set; }
    }
}