namespace Footballize.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Exceptions;
    using Footballize.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class TeamService : ITeamService
    {
        private const string InvalidJoinPasswordMessage = "Cannot join this team beaucase password is invalid";

        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IRepository<TeamUser> teamUserRepository;

        public TeamService(IDeletableEntityRepository<Team> teamRepository, IRepository<TeamUser> teamUserRepository)
        {
            this.teamRepository = teamRepository;
            this.teamUserRepository = teamUserRepository;
        }

        public IEnumerable<TViewModel> GetTeams<TViewModel>()
        {
            return this.teamRepository
                .All()
                .To<TViewModel>();
        }

        public async Task CreateTeamAsync(Team team)
        {
            if (team == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            var gatherUser = new TeamUser
            {
                Team = team,
                User = team.Owner
            };

            team.Players.Add(gatherUser);

            var teamPasswordHash = this.HashPassword(team.Password);
            team.Password = teamPasswordHash;

            await this.teamRepository.AddAsync(team);
            await this.teamRepository.SaveChangesAsync();
        }

        public TViewModel GetTeam<TViewModel>(string id)
        {
            return this.teamRepository
                .All()
                .Where(x => x.Id.Equals(id))
                .To<TViewModel>()
                .SingleOrDefault();
        }

        public async Task DeleteTeamAsync(Team team)
        {
            if (team == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            this.teamRepository.Delete(team);
            await this.teamRepository.SaveChangesAsync();
        }

        public async Task JoinTeamAsync(Team team, string joinPassword, User user)
        {
            var isPasswordValid = this.TryValidatePassword(joinPassword, team.Password);

            if (user == null)
            {
                throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            if (!isPasswordValid)
            {
                throw  new ServiceException(InvalidJoinPasswordMessage);
            }

            var teamRooster = new TeamUser
            {
                User = user,
                Team = team
            };

            team.Players.Add(teamRooster);
            this.teamRepository.Update(team);
            await this.teamRepository.SaveChangesAsync();
        }


        public async Task<Team> GetTeamAsync(string id)
        {
            return  await this.teamRepository.GetByIdAsync(id);
        }

        public Team GetTeamWithPlayers(string id)
        {
            return this.teamRepository
                .All()
                .Include(x => x.Players)
                .SingleOrDefault(x => x.Id == id);
        }

        public async Task LeaveTeamAsync(Team team, string playerId)
        {
            if (team == null)
            {
                throw  new ServiceException(ServiceException.InvalidRequestParameters);
            }

            var rooster = team.Players.SingleOrDefault(x => x.UserId == playerId);

            if (rooster == null)
            {
               throw new ServiceException(ServiceException.PlayerIsNotPartOfTheGame);
            }

            this.teamUserRepository.Delete(rooster);
            team.Players.Remove(rooster);
            this.teamRepository.Update(team);

            await this.teamUserRepository.SaveChangesAsync();
            await this.teamRepository.SaveChangesAsync();
        }

        public async  Task UpdateTeamPassword(Team team, string newPassword)
        {
            if (team == null || string.IsNullOrEmpty(newPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
               throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            team.Password = this.HashPassword(newPassword);
            this.teamRepository.Update(team);
            await this.teamRepository.SaveChangesAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            if (team == null)
            {
               throw new ServiceException(ServiceException.InvalidRequestParameters);
            }

            this.teamRepository.Update(team);
            await this.teamRepository.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private bool TryValidatePassword(string inputPassword, string hashedPassword)
        {
            var hashedInput = this.HashPassword(inputPassword);
            var comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashedInput, hashedPassword) == 0;
        }
    }
}