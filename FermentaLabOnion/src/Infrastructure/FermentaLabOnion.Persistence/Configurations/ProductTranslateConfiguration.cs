using FermentaLabOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Configurations
{
    public class ProductTranslateConfiguration : IEntityTypeConfiguration<ProductTranslate>
    {
        public void Configure(EntityTypeBuilder<ProductTranslate> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.ShortDescription)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(x => x.QuantityPerPackage)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Volume)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PackageType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Ingredients)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Benefits)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.UsageInstructions)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Warnings)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Brand)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.CountryOfOrigin)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.ShelfLife)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
