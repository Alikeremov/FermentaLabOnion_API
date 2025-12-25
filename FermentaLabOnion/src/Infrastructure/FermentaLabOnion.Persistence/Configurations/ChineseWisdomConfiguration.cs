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
    public class ChineseWisdomConfiguration : IEntityTypeConfiguration<ChineseWisdom>
    {
        public void Configure(EntityTypeBuilder<ChineseWisdom> builder)
        {
            builder.Property(x => x.Tittle)
             .IsRequired()
             .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(4000);
        }
    }
}
