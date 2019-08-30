namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Abstracts;
    using Enums;

    public class Gather : BaseMatchModel
    {
        public Gather()
        {
            this.Status = GameStatus.Registration;
            this.Players = new HashSet<GatherUser>();
            this.ChatMessages = new HashSet<ChatMessage>();
        }

        public string Description { get; set; }
        public int MaximumPlayers { get; set; }
        public TeamFormat TeamFormat { get; set; }
        public GameStatus Status { get; set; }
        public ICollection<GatherUser> Players { get; set; }
        public ICollection<ChatMessage> ChatMessages{ get; set; }
    }
}