namespace Footballize.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Footballize.Models;
    using Models.Recruitment;

    public interface IRecruitmentService : IService
    {
        IEnumerable<TViewModel> GetAll<TViewModel>();
        IEnumerable<TViewModel> GetAll<TViewModel>(Expression<Func<Recruitment, bool>> expression);
        TViewModel GetById<TViewModel>(string id);
        Task<RecruitmentServiceModel> GetByIdAsync(string id);
        Task<string> AddAsync(string title, DateTime startingAt, string pitchId, string creatorId, int maximumPlayers);
        Task LeaveAsync(string gameId, string userId);
        Task EnrollAsync(string gameId, string userId);
        Task StartAsync(string id);
        Task CompleteAsync(string id);
        Task DeleteAsync(string id);
        bool Exists(string id);
        int GetCount();
    }
}