namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Abstracts;
    using Enums;

    public class Gather : BaseDeletableModel<string>
    {
        public Gather()
        {
            this.Players = new HashSet<GatherUser>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual Pitch Pitch { get; set; }
        public string PitchId { get; set; }
        public TeamFormat TeamFormat { get; set; }
        public GameStatus Status { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public int MaximumPlayersAllowed { get; set; }
        public ICollection<GatherUser> Players { get; set; }
    }
}