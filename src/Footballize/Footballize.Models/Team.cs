namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;
    using Enums;

    public class Team : BaseDeletableModel<string>
    {
        public Team()
        {
            this.Players = new HashSet<TeamUser>();
        }

        public string Name { get; set; }
        public string CountryId { get; set; }
        public virtual Country Nationality { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public bool IsBanned { get; set; }
        public string Password { get; set; }
        public virtual ICollection<TeamUser> Players { get; set; }
        //TODO: Add Matches
    }
}