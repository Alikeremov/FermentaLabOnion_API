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
    public class HeritageProcessConfiguration : IEntityTypeConfiguration<HeritageProcess>
    {
        public void Configure(EntityTypeBuilder<HeritageProcess> builder)
        {
            builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(250);
            builder.Property(x => x.BeforeLabel)
            .IsRequired()
            .HasMaxLength(30);
            builder.Property(x => x.AfterLabel)
            .IsRequired()
            .HasMaxLength(30);
        }
    }
}
