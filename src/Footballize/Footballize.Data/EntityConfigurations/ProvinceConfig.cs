﻿namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ProvinceConfig : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(x => x.Id);;

            builder.HasMany(p => p.Towns)
                .WithOne(m => m.Province);

            builder.Property(p => p.Name)
                .IsUnicode()
                .HasColumnType("NVARCHAR(75)")
                .HasMaxLength(75)
                .IsRequired();
        }
    }
}