namespace Footballize.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    public class RolesSeeder : ISeeder
    {
        public void Seed(FootballizeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roles = new List<Role>
            {
                new Role(GlobalConstants.CanCreateGathersRoleName),
                new Role(GlobalConstants.CanEditGathersRoleName),
                new Role(GlobalConstants.CanDeleteGathersRoleName),
                new Role(GlobalConstants.CanCreateRecruitmentRoleName),
                new Role(GlobalConstants.CanEditRecruitmentRoleName),
                new Role(GlobalConstants.CanDeleteRecruitmentRoleName),
                new Role(GlobalConstants.CanSeeAdminAreaRoleName),
            };

            var roleManager = serviceProvider.GetService<RoleManager<Role>>();

            foreach (Role role in roles)
            {
                if (!roleManager.RoleExistsAsync(role.Name).Result)
                {
                    IdentityResult roleResult = roleManager.
                        CreateAsync(role).Result;
                }
            }
        }
    }
}