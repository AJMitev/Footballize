namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IRecruitmentService
    {
        ICollection<TViewModel> GetRecruitments<TViewModel>();
        TViewModel GetRecruitment<TViewModel>(string id);
        Task AddRecruitmentAsync(Recruitment recruitment);
        Task LeaveRecruitmentAsync(string recruitmentId, string userId);
        Task EnrollRecruitmentAsync(string recruitmentId, string userId);
        Task StartRecruitmentAsync(string id);
        Task CompleteRecruitmentAsync(string id);
        Task DeleteRecruitmentAsync(string id);
    }
}