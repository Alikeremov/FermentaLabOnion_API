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
    public class AppTranslateConfiguration : IEntityTypeConfiguration<AppTranslate>
    {
        public void Configure(EntityTypeBuilder<AppTranslate> builder)
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
