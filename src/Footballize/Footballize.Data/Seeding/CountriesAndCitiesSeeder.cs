namespace Footballize.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class CountriesAndCitiesSeeder : ISeeder
    {
        public void Seed(FootballizeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var sources = new List<Country>
            {
                new Country
                {
                    Name = "Bulgaria",
                    IsoCode = "BG",
                    Provinces = new List<Province>
                    {
                        new Province
                        {
                            Name = "Blagoevgrad",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Blagoevgrad",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Burgas",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Burgas",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Dobrich",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Dobrich",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Gabrovo",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Gabrovo",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Kardzhali",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Kardzhali",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Khaskovo",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Khaskovo",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Kyustendil",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Kyustendil",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Lovec",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Lovec",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Montana",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Montana",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Pazardzhik",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Pazardzhik",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Pernik",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Pernik",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Pleven",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Pleven",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Plovdiv",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Plovdiv",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Razgrad",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Razgrad",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Ruse",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Ruse",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Shumen",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Shumen",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Silistra",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Silistra",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Sliven",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Sliven",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Smolyan",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Smolyan",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Sofia",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Sofia",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Stara Zagora",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Stara Zagora",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Targovishte",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Targovishte",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Veliko Turnovo",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Veliko Turnovo",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Varna",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Varna",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Vidin",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Vidin",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Vratsa",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Vratsa",
                                }
                            }
                        },
                        new Province
                        {
                            Name = "Yambol",
                            Towns = new List<Town>
                            {
                                new Town
                                {
                                    Name = "Yambol",
                                }
                            }
                        }
                    }
                }
            };

            foreach (Country country in sources)
            {
                if (!dbContext.Countries.Any(x=>x.Name == country.Name))
                {
                    dbContext.Countries.Add(country);
                }
            }
        }
    }
}