namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Models.Recruitment;

    public interface IRecruitmentService : IService
    {
        ICollection<TViewModel> GetAll<TViewModel>();
        ICollection<TViewModel> GetAll<TViewModel>(Expression<Func<Recruitment, bool>> expression);
        TViewModel GetById<TViewModel>(string id);
        Task<RecruitmentServiceModel> GetByIdAsync(string id);
        Task AddAsync(string title, DateTime startingAt, string pitchId, string creatorId, int maximumPlayers, GameStatus gameStatus);
        Task LeaveAsync(string gameId, string userId);
        Task EnrollAsync(string gameId, string userId);
        Task StartAsync(string id);
        Task CompleteAsync(string id);
        Task DeleteAsync(string id);
        bool Exist(string id);
    }
}