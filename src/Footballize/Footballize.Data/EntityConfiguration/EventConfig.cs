namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .HasColumnType("NVARCHAR(30)")
                .HasMaxLength(30)
                .IsUnicode();
        }
    }
}