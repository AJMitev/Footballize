namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnType("NVARCHAR(30)")
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnType("NVARCHAR(30)")
                .IsUnicode()
                .IsRequired();

            builder.HasIndex(e => e.UserName)
                .IsUnique();

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}