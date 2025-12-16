using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.TagTranslateDTOs
{
    public record TagTranslateCreateDto
    {
        public string Name { get; set; } = null!;
        public int? TagId { get; set; }
        public Language Language { get; set; }
    }
}
