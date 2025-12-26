using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.AppTranslateDTOs
{
    public record AppTranslateCreateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int AppId { get; set; }
        public Language Language { get; set; }
    }
}
