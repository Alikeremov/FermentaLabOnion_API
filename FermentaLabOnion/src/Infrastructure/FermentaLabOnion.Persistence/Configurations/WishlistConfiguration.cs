using FermentaLabOnion.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AppUserId).HasMaxLength(450);
            builder.Property(x => x.CookieId).HasMaxLength(64);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Wishlist)
                .HasForeignKey(x => x.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.AppUserId)
                .IsUnique()
                .HasFilter("[AppUserId] IS NOT NULL");

            builder.HasIndex(x => x.CookieId)
                .IsUnique()
                .HasFilter("[CookieId] IS NOT NULL");

            
        }
    }
}
