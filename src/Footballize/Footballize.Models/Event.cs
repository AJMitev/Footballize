namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Abstracts;
    using Enums;

    public class Event : BaseDeletableModel<string>
    {
        public Event()
        {
            this.Status = GameStatus.Registration;

            this.Players = new HashSet<EventUser>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual Pitch Pitch { get; set; }
        public string PitchId { get; set; }
        public TeamFormat TeamFormat { get; set; }
        public GameStatus Status { get; set; }  

        public virtual ICollection<EventUser> Players { get; set; }
    }
}