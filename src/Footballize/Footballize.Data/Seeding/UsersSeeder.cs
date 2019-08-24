namespace Footballize.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footballize.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    public class UsersSeeder : ISeeder
    {
        public void Seed(FootballizeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var users = new List<User>
            {
                new User
                {
                    Email = "test@aleksandar.it",
                    UserName = "MyTestUser",
                    FirstName = "Tester",
                    LastName = "Testerov"
                },
                new User
                {
                    Email = "gather@aleksandar.it",
                    UserName = "GatherAdmin",
                    FirstName = "Gather",
                    LastName = "Admin"
                },
                new User
                {
                    Email = "recruit@aleksandar.it",
                    UserName = "RecruitAdmin",
                    FirstName = "Recruit",
                    LastName = "Admin"
                },
                new User
                {
                    Email = "super@aleksandar.it",
                    UserName = "SuperAdmin",
                    FirstName = "Super",
                    LastName = "Admin"
                }
            };

            foreach (var user in users)
            {
                if (userManager.FindByEmailAsync(user.Email).Result == null)
                {
                    IdentityResult result = userManager.CreateAsync(user, "gather123").Result;

                    if (result.Succeeded)
                    {
                        if (user.UserName.Contains("Gather"))
                        {
                            userManager.AddToRoleAsync(user, GlobalConstants.CanCreateGathersRoleName).Wait(); ;
                            userManager.AddToRoleAsync(user, GlobalConstants.CanEditGathersRoleName).Wait(); ;
                            userManager.AddToRoleAsync(user, GlobalConstants.CanDeleteGathersRoleName).Wait(); ;
                        }

                        if (user.UserName.Contains("Recruit"))
                        {
                            userManager.AddToRoleAsync(user, GlobalConstants.CanCreateRecruitmentRoleName).Wait(); ;
                            userManager.AddToRoleAsync(user, GlobalConstants.CanEditRecruitmentRoleName).Wait(); ;
                            userManager.AddToRoleAsync(user, GlobalConstants.CanDeleteRecruitmentRoleName).Wait(); ;
                        }

                        if (user.UserName.Contains("Super"))
                        {
                            userManager.AddToRoleAsync(user, GlobalConstants.CanCreateGathersRoleName).Wait();
                            userManager.AddToRoleAsync(user, GlobalConstants.CanEditGathersRoleName).Wait();
                            userManager.AddToRoleAsync(user, GlobalConstants.CanDeleteGathersRoleName).Wait();
                            userManager.AddToRoleAsync(user, GlobalConstants.CanCreateRecruitmentRoleName).Wait();
                            userManager.AddToRoleAsync(user, GlobalConstants.CanEditRecruitmentRoleName).Wait();
                            userManager.AddToRoleAsync(user, GlobalConstants.CanDeleteRecruitmentRoleName).Wait();
                            userManager.AddToRoleAsync(user, GlobalConstants.CanSeeAdminAreaRoleName).Wait();
                        }
                    }
                }
            }
        }

    }
}