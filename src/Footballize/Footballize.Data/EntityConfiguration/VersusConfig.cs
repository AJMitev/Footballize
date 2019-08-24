namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class VersusConfig : IEntityTypeConfiguration<Versus>
    {
        public void Configure(EntityTypeBuilder<Versus> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeMatches)
                .HasForeignKey(x => x.HomeTeamId);

            builder.HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayMatches)
                .HasForeignKey(x => x.AwayTeamId);
        }
    }
}