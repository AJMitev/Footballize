namespace Footballize.Models
{
    using System;
    using Interfaces;

    public class RecruitmentUser : IDeletableEntity, IAuditInfo
    {
        public string RecruitmentId { get; set; }
        public Recruitment Recruitment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}