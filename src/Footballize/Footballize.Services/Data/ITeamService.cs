namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITeamService
    {
        IEnumerable<TViewModel> GetTeams<TViewModel>();

        Task CreateTeamAsync(Team team);
    }
}