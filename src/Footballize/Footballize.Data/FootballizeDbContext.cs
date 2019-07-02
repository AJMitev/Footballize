namespace Footballize.Data
{
    using EntityConfiguration;
    using Footballize.Models;
    using Microsoft.EntityFrameworkCore;

    public class FootballizeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Pitch> Pitches { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Playfield> Playfields { get; set; }
        public DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbSettings.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfig());
            modelBuilder.ApplyConfiguration(new CountryConfig());
            modelBuilder.ApplyConfiguration(new EventConfig());
            modelBuilder.ApplyConfiguration(new MunicipalityConfig());
            modelBuilder.ApplyConfiguration(new PitchConfig());
            modelBuilder.ApplyConfiguration(new PlayfieldConfig());
            modelBuilder.ApplyConfiguration(new ProvinceConfig());
            modelBuilder.ApplyConfiguration(new TownConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}