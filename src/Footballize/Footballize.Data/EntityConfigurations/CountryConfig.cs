namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(e => e.Provinces)
                .WithOne(e => e.Country);

            builder.Property(e => e.Name)
                .HasColumnType("NVARCHAR(30)")
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.IsoCode)
                .HasColumnType("VARCHAR(5)")
                .HasMaxLength(5)
                .IsRequired();
        }
    }
}