namespace Footballize.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.Enums;

    public class GamesSeeder : ISeeder
    {
        public void Seed(FootballizeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var user = dbContext.Users.FirstOrDefault();
            var fields = dbContext.Pitches.Take(2).ToList();
            this.SeedGathers(dbContext, user, fields);
            this.SeedRecruitments(dbContext, user, fields);
        }

        private void SeedRecruitments(FootballizeDbContext dbContext, User user, List<Pitch> fields)
        {
            var games = new List<Recruitment>();

            for (int i = 2; i <= 10; i++)
            {
                var index = i % 2 == 0 ? 1 : 0;

                var game = new Recruitment
                {
                    Title = $"Need {i} players",
                    Pitch = fields[index],
                    Creator = user,
                    StartingAt = DateTime.Now.AddDays(2),
                    MaximumPlayers = i,
                };

                games.Add(game);
            }

            foreach (var game in games)
            {
                dbContext.Recruitments.Add(game);
            }
        }

        private void SeedGathers(FootballizeDbContext dbContext, User user, List<Pitch> fields)
        {
            var gathers = new List<Gather>();


            for (int i = 0; i < 10; i++)
            {
                var index = i % 2 == 0 ? 1 : 0;
                var gather = new Gather
                {
                    Pitch = fields[index],
                    Creator = user,
                    StartingAt = DateTime.Now.AddDays(1),
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lectus nisi, dictum vitae euismod vel, elementum eu nunc. Maecenas feugiat orci enim, a euismod mauris sollicitudin et.",
                    TeamFormat = TeamFormat.FivePlusOne,
                    Title = $"Game {(i + 1)}",
                    MaximumPlayers = 12
                };

                var player = new GatherUser
                {
                    Gather = gather,
                    User = user
                };

                gather.Players.Add(player);
                gathers.Add(gather);
            }


            foreach (Gather gather in gathers)
            {
                if (!dbContext.Countries.Any(x => x.Name == gather.Title))
                {
                    dbContext.Gathers.Add(gather);

                }
            }
        }
    }
}