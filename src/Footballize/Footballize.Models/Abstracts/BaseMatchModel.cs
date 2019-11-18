namespace Footballize.Models.Abstracts
{
    using System;
    using System.Collections.Generic;

    public abstract class BaseMatchModel : BaseDeletableModel<string>
    {
        public string Title { get; set; }
        public DateTime StartingAt { get; set; }
        public Pitch Pitch { get; set; }
        public string PitchId { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
    }
}