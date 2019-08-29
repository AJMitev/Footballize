namespace Footballize.Web.ViewModels.Gathers
{
    using System;
    using System.Collections.Generic;
    using Areas.Administration.ViewModels.Pitches;
    using Models;
    using Models.Enums;
    using Services.Mapping;
    using Shared;
    using Users;

    public class GatherDetailsViewModel : IMapFrom<Gather>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartingAt { get; set; }
        public PitchIndexViewModel Pitch { get; set; }
        public string PitchId { get; set; }
        public TeamFormat TeamFormat { get; set; }
        public GameStatus Status { get; set; }
        public  User Creator { get; set; }
        public int MaximumPlayers { get; set; }
        public ICollection<UserGameDetailsViewModel> Players { get; set; }
    }
}