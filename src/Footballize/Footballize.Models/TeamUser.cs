namespace Footballize.Models
{
    using System;
    using Interfaces;

    public class TeamUser : IDeletableEntity, IAuditInfo
    {
        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}