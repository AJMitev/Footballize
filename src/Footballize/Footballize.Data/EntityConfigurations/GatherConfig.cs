namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class GatherConfig : IEntityTypeConfiguration<Gather>
    {
        public void Configure(EntityTypeBuilder<Gather> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Title)
                .HasColumnType("NVARCHAR(30)")
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.Description)
                .HasColumnType("NVARCHAR(300)")
                .HasMaxLength(300)
                .IsUnicode();
        }
    }
}