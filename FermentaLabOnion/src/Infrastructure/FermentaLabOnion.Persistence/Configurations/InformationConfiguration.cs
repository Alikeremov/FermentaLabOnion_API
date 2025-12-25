using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FermentaLabOnion.Domain.Entities;

namespace FermentaLabOnion.Persistence.Configurations
{
    public class InformationConfiguration : IEntityTypeConfiguration<Information>
    {
        public void Configure(EntityTypeBuilder<Information> builder)
        {
            builder.Property(x => x.Tittle)
            .IsRequired()
            .HasMaxLength(200);
            builder.Property(x => x.Image)
                .IsRequired()
                .HasMaxLength(500);

        }
    }
}
