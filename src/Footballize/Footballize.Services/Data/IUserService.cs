namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Models;

    public interface IUserService : IService
    {
        ICollection<TViewModel> GetUsers<TViewModel>();
        Task<User> GetUserAsync(string id);
        User GetUser(string id);
        Task AddPlaypal(User userToAdd, User currentUser);
        Task RemovePlaypal(string playpalId, string currentUserId);
        Task BanPlayer(User player, int minutes);
        Task RemoveBan(User player);
        Task CreateReport(UserReport report);
        int GetUsersCount();
        ICollection<TViewModel> GetUserReports<TViewModel>();
        ICollection<TViewModel> GetUsers<TViewModel>(Expression<Func<User, bool>> expression);
    }
}