namespace Footballize.Services.Models.Recruitment
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Mapping;
    using Pitch;
    using User;

    public class RecruitmentServiceModel : IMapFrom<Recruitment>
    {
        public RecruitmentServiceModel() 
            => this.Players = new HashSet<RecruitmentUserServiceModel>();

        public string Title { get; set; }
        public DateTime StartingAt { get; set; }
        public PitchServiceModel Pitch { get; set; }
        public string PitchId { get; set; }
        public string CreatorId { get; set; }
        public UserServiceModel Creator { get; set; }
        public int MaximumPlayers { get; set; }
        public GameStatus Status { get; set; }
        public ICollection<RecruitmentUserServiceModel> Players { get; set; }
    }
}