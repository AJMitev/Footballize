namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class EventUserConfig : IEntityTypeConfiguration<EventUser>
    {
        public void Configure(EntityTypeBuilder<EventUser> builder)
        {
            builder.HasKey(e => new {e.UserId, e.EventId});

            builder.HasOne(e => e.User)
                .WithMany(e => e.GamesPlayed)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Event)
                .WithMany(e => e.Players)
                .HasForeignKey(e => e.EventId);
        }
    }
}