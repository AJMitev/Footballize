namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserReportConfig : IEntityTypeConfiguration<UserReport>
    {
        public void Configure(EntityTypeBuilder<UserReport> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Text)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(350)
                .HasColumnType("NVARCHAR(350)");

            builder.Property(x => x.ReportedUserId)
                .IsRequired();
        }
    }
}