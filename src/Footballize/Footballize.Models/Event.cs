namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;

    public class Event
    {
        public Event()
        {
            this.HomeTeam = new HashSet<User>();
            this.AwayTeam = new HashSet<User>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingAt { get; set; }
        public TimeSpan Duration { get; set; }
        public Pitch Pitch { get; set; }
        public string PitchId { get; set; }
        public TeamFormat TeamFormat { get; set; }

        public ICollection<User> HomeTeam { get; set; }
        public ICollection<User> AwayTeam { get; set; }
    }
}