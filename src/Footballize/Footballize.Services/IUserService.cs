namespace Footballize.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Footballize.Models;
    using Footballize.Models.Enums;
    using Models.User;

    public interface IUserService : IService
    {
        IEnumerable<TViewModel> GetAll<TViewModel>();
        IEnumerable<TViewModel> GetAll<TViewModel>(Expression<Func<User, bool>> expression);
        Task<TViewModel> GetByIdAsync<TViewModel>(string id);
        UserServiceModel GetById(string id);
        Task AddPlaypal(string playpalId, string currentUserId);
        Task RemovePlaypal(string playpalId, string currentUserId);
        Task BanPlayer(string userId, int minutes);
        Task RemoveBan(string userId);
        Task CreateReport(string text, ReportType type, string reportedUserId);
        int GetUsersCount();
        IEnumerable<TViewModel> GetUserReports<TViewModel>();
        bool Exists(string id);
        IEnumerable<T> GetInactiveUsers<T>(int days);
    }
}