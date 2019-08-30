namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Models;

    public interface IRecruitmentService
    {
        ICollection<TViewModel> GetRecruitments<TViewModel>();
        ICollection<TViewModel> GetRecruitments<TViewModel>(Expression<Func<Recruitment, bool>> expression);
        TViewModel GetRecruitment<TViewModel>(string id);
        Recruitment GetRecruitmentWithPlayers(string id);
        Task<Recruitment> GetRecruitmentAsync(string id);
        Task AddRecruitmentAsync(Recruitment recruitment);
        Task LeaveRecruitmentAsync(Recruitment recruitment, string userId);
        Task EnrollRecruitmentAsync(Recruitment recruitment, User user);
        Task StartRecruitmentAsync(string id);
        Task CompleteRecruitmentAsync(string id);
        Task DeleteRecruitmentAsync(string id);
    }
}