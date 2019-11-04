namespace Footballize.Web.ViewModels.Recruitments
{
    using System;
    using Models;
    using Models.Enums;
    using Services.Mapping;

    public class RecruitmentUserViewModel : IMapFrom<RecruitmentUser>
    {
        public string RecruitmentId { get; set; }
        public string RecruitmentTitle { get; set; }
        public GameStatus RecruitmentStatus { get; set; }
        public DateTime RecruitmentStartingAt { get; set; }
        public int RecruitmentMaximumPlayers { get; set; }
    }
}