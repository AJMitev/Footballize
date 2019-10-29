namespace Footballize.Services.Models.Recruitment
{ 
    using Footballize.Models;
    using Mapping;

    public class RecruitmentUserServiceModel : IMapFrom<Recruitment>
    {
        public string RecruitmentId { get; set; }
        public RecruitmentServiceModel Recruitment { get; set; }
        public string UserId { get; set; }
        public RecruitmentUserServiceModel User { get; set; }
    }
}