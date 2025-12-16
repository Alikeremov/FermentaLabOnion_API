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
    public class CategoryTranslateConfiguration : IEntityTypeConfiguration<CategoryTranslate>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslate> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(202);
        }
    }
}
