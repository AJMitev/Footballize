﻿namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Province)
                .WithMany(e => e.Towns)
                .HasForeignKey(e => e.ProvinceId);

            builder.HasMany(e => e.Addresses)
                .WithOne(e => e.Town);

            builder.Property(e => e.Name)
                .HasColumnType("NVARCHAR(30)")
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode();
        }
    }
}