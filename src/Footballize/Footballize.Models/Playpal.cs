namespace Footballize.Models
{
    using System;
    using Interfaces;

    public class Playpal : IDeletableEntity, IAuditInfo
    {
        public string FromUserId { get; set; }
        public virtual User FromUser { get; set; }
        public string ToUserId { get; set; }
        public virtual User ToUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}