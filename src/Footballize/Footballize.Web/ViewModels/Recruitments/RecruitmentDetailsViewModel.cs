namespace Footballize.Web.ViewModels.Recruitments
{
    using System;
    using System.Collections.Generic;
    using Areas.Administration.ViewModels.Pitches;
    using Models;
    using Models.Enums;
    using Services.Mapping;
    using Users;

    public class RecruitmentDetailsViewModel : IMapFrom<Recruitment>, IMapTo<Recruitment>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime StartingAt { get; set; }
        public PitchIndexViewModel Pitch { get; set; }
        public string PitchId { get; set; }
        public GameStatus Status { get; set; }
        public User Creator { get; set; }
        public int MaximumPlayers { get; set; }
        public ICollection<UserGameDetailsViewModel> Players { get; set; }
    }
}