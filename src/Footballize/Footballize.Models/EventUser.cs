﻿namespace Footballize.Models
{
    using System;
    using Abstracts;
    using Interfaces;

    public class GatherUser : IDeletableEntity, IAuditInfo
    {
        public string GatherId { get; set; }
        public virtual Gather Gather { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}