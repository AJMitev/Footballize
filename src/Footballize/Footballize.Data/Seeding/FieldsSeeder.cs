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
            var sofiaId = dbContext.Towns.SingleOrDefault(x => x.Name.Equals("Sofia")).Id;

            var fields = new List<Pitch>
            {
                new Pitch
                {
                    Name = "Atrim Arena",
                    Address = new Address
                    {
                        TownId = plovdivId,
                        Street = "Carigradko Shose blvd.",
                        Number = 54,
                        Location = new Location
                        {
                            Latitude = 42.150069,
                            Longitude = 24.799438
                        }
                    }

                },
                new Pitch
                {
                    Name = "SC Optimum",
                    Address = new Address
                    {
                        TownId = plovdivId,
                        Street = "Asenovgradsko Shose blvd.",
                        Number = 116,
                        Location = new Location
                        {
                            Latitude = 42.125237,
                            Longitude = 24.772354
                        }
                    }
                },
                new Pitch
                {
                    Name = "Korona",
                    Address = new Address
                    {
                        TownId = plovdivId,
                        Street = "Car Simeon",
                        Number = 158,
                        Location = new Location
                        {
                            Latitude = 42.127130,
                            Longitude = 24.781652
                        }
                    }
                },
                new Pitch
                {
                    Name = "Select",
                    Address = new Address
                    {
                        TownId = plovdivId,
                        Street = "Peroshtica",
                        Number = 6,
                        Location = new Location
                        {
                            Latitude = 42.1402311,
                            Longitude = 24.7184506
                        }
                    }

                },
                new Pitch
                {
                    Name = "RD Sport",
                    Address = new Address
                    {
                        TownId = sofiaId,
                        Street = "Studentski Kompleks",
                        Number = 1756,
                        Location = new Location
                        {
                            Latitude = 42.659793,
                            Longitude = 23.354340
                        }
                    }
                },
                new Pitch
                {
                    Name = "Sofia Sport",
                    Address = new Address
                    {
                        TownId = sofiaId,
                        Street = "Fransoa Miteran",
                        Number = 1700,
                        Location = new Location
                        {
                            Latitude = 42.649877,
                            Longitude = 23.339989
                        }
                    }
                },
                new Pitch
                {
                    Name = "Arena Royal",
                    Address = new Address
                    {
                        TownId = sofiaId,
                        Street = "Dimitar Pashev",
                        Number = 158,
                        Location = new Location
                        {
                            Latitude = 42.645417,
                            Longitude = 23.412855
                        }
                    }
                },
                new Pitch
                {
                    Name = "Green Sport",
                    Address = new Address
                    {
                        TownId = sofiaId,
                        Street = "Dimitar Pashev",
                        Number = 158,
                        Location = new Location
                        {
                            Latitude = 42.676759,
                            Longitude = 23.380542
                        }
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