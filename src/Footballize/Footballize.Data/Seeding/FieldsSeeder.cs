namespace Footballize.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class FieldsSeeder : ISeeder
    {

        public void Seed(FootballizeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var plovdivId = dbContext.Towns.SingleOrDefault(x => x.Name.Equals("Plovdiv")).Id;

            var fields = new List<Pitch>
            {
                new Pitch
                {
                    Name = "Atrim Arena",
                    Address = new Address
                    {
                        TownId = plovdivId,
                        Street = "Carigradko Shose blvd.",
                        Number = 54
                    }

                },
                new Pitch
                {
                    Name = "SC Optimum",
                    Address = new Address
                    {
                        TownId = plovdivId,
                        Street = "Asenovgradsko Shose blvd.",
                        Number = 116
                    }
                }
            };


            foreach (Pitch field in fields)
            {
                if (!dbContext.Pitches.Any(x => x.Name == field.Name))
                {
                    dbContext.Pitches.Add(field);
                }
            }
        }
    }
}