using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.CategoryTranslateDTOs
{
    public record CategoryTranslateCreateDto
    {
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public Language Language { get; set; }
    }
}
