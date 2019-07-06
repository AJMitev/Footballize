namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PlayfieldConfig : IEntityTypeConfiguration<Playfield>
    {
        public void Configure(EntityTypeBuilder<Playfield> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .HasColumnType("NVARCHAR(50)")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();
        }
    }
}