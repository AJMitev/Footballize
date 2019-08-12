namespace Footballize.Models
{
    using System.Collections.Generic;
    using Abstracts;

    public class Recruitment : BaseMatchModel
    {
        public ICollection<RecruitmentUser> RecruitedUsers { get; set; }
    }
}