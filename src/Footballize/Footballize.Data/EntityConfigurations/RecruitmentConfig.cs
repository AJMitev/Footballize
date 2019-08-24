namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class RecruitmentConfig : IEntityTypeConfiguration<Recruitment>
    {
        public void Configure(EntityTypeBuilder<Recruitment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.MaximumPlayers)
                .IsRequired();
        }
    }
}