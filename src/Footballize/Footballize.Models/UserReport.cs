namespace Footballize.Models
{
    using System;
    using Abstracts;
    using Enums;

    public class UserReport : BaseDeletableModel<string>
    {
        public UserReport()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ReportType Type { get; set; }
        public string Text { get; set; }
        public string ReportedUserId { get; set; }
        public User ReportedUser { get; set; }
    }
}