namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IUserService
    {
        ICollection<TViewModel> GetUsers<TViewModel>();
        Task<User> GetUserAsync(string id);
        User GetUser(string id);
        Task AddPlaypal(User userToAdd, User currentUser);
        Task RemovePlaypal(string playpalId, string currentUserId);
        Task BanPlayer(User player, int minutes);
        Task RemoveBan(User player);
        Task CreateReport(UserReport report);
    }
}