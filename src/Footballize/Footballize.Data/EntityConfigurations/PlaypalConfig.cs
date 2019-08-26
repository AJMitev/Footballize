namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PlaypalConfig : IEntityTypeConfiguration<Playpal>
    {
        public void Configure(EntityTypeBuilder<Playpal> builder)
        {
            builder.HasKey(x => new { x.FromUserId, x.ToUserId});

            builder.HasOne(x => x.FromUser)
                .WithMany(x => x.PlaypalsAdded)
                .HasForeignKey(x => x.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ToUser)
                .WithMany(x => x.PlaypalsAddedMe)
                .HasForeignKey(x => x.ToUserId);
        }
    }
}