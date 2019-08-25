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
        Task AddPlaypal(User playpal, User currentUser);
        Task RemovePlaypal(User playpal, User currentUser);
    }
}