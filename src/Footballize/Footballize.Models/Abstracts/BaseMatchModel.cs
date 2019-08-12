namespace Footballize.Models.Abstracts
{
    using System;
    using System.Collections.Generic;

    public abstract class BaseMatchModel : BaseDeletableModel<string>
    {
        public string Title { get; set; }
        public DateTime StartingAt { get; set; }
        public virtual Pitch Pitch { get; set; }
        public string PitchId { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public int MaximumPlayers { get; set; }
    }
}