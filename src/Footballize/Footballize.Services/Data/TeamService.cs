namespace Footballize.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Footballize.Data.Repositories;
    using Mapping;
    using Models;

    public class TeamService : ITeamService
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public TeamService(IDeletableEntityRepository<Team> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public IEnumerable<TViewModel> GetTeams<TViewModel>()
        {
            return this.teamRepository
                .All()
                .To<TViewModel>();
        }

        public async Task CreateTeamAsync(Team team)
        {
            var gatherUser = new TeamUser
            {
                Team = team,
                User = team.Owner
            };

            team.Players.Add(gatherUser);


            await this.teamRepository.AddAsync(team);
            await this.teamRepository.SaveChangesAsync();
        }
    }
}