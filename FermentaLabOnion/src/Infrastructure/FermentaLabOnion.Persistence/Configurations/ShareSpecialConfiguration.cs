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
    public class ShareSpecialConfiguration : IEntityTypeConfiguration<ShareSpecial>
    {
        public void Configure(EntityTypeBuilder<ShareSpecial> builder)
        {
            builder.Property(x => x.Title)
             .IsRequired()
             .HasMaxLength(200);

            builder.Property(x => x.Subtitle)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
