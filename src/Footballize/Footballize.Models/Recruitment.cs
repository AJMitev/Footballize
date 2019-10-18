namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Abstracts;
    using Enums;

    public class Recruitment : BaseMatchModel
    {
        public Recruitment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Players = new List<RecruitmentUser>();
            this.Status = GameStatus.Registration;
        }

        public int MaximumPlayers { get; set; }
        public GameStatus Status { get; set; }
        public ICollection<RecruitmentUser> Players { get; set; }
    }
}