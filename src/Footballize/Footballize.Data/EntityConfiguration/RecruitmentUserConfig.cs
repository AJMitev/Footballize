namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class RecruitmentUserConfig : IEntityTypeConfiguration<RecruitmentUser>
    {
        public void Configure(EntityTypeBuilder<RecruitmentUser> builder)
        {
            builder.HasKey(e => new {e.UserId, EventId = e.RecruitmentId});

            builder.HasOne(e => e.User)
                .WithMany(e => e.GamesRecruited)
                .HasForeignKey(e => e.UserId);
        }
    }
}