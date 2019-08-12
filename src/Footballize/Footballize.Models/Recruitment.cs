namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;
    using Enums;

    public class Recruitment : BaseMatchModel
    {
        public Recruitment()
        {
            this.RecruitedUsers = new List<RecruitmentUser>();
            this.Status = GameStatus.Registration;
        }

        public GameStatus Status { get; set; }
        public ICollection<RecruitmentUser> RecruitedUsers { get; set; }
    }
}