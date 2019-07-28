namespace Footballize.Data.Seeding
{
    using System;

    public interface ISeeder
    {
        void Seed(FootballizeDbContext dbContext, IServiceProvider serviceProvider);
    }
}