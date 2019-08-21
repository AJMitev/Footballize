using System;

namespace Footballize.Data.Seeding
{
    using Models;

    public class UsersSeeder : ISeeder
    {
        public void Seed(FootballizeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var testUser = new User
            {
                Email = "test@aleksandar.it",
                UserName = "MyTestUser",
                FirstName = "Tester",
                LastName = "Testerov"
            };

            dbContext.Users.Add(testUser);
        }
    }
}