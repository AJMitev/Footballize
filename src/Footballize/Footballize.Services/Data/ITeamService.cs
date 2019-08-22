namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITeamService
    {
        IEnumerable<TViewModel> GetTeams<TViewModel>();
        TViewModel GetTeam<TViewModel>(string id);
        Team GetTeamWithPlayers(string id);
        Task<Team> GetTeamAsync(string id);

        Task CreateTeamAsync(Team team);
        Task DeleteTeamAsync(Team team);
        Task JoinTeamAsync(Team team, string joinPassword, User user);
        Task LeaveTeamAsync(Team team, User player);
    }
}