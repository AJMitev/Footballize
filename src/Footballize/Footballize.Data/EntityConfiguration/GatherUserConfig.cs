namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class GatherUserConfig : IEntityTypeConfiguration<GatherUser>
    {
        public void Configure(EntityTypeBuilder<GatherUser> builder)
        {
            builder.HasKey(e => new {e.UserId, EventId = e.GatherId});

            builder.HasOne(e => e.User)
                .WithMany(e => e.GamesPlayed)
                .HasForeignKey(e => e.UserId);
        }
    }
}