namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(x => x.Street)
                .HasMaxLength(300)
                .IsRequired();

            builder.HasOne(a => a.Town)
                .WithMany(t => t.Addresses)
                .HasForeignKey(a => a.TownId);
        }
    }
}