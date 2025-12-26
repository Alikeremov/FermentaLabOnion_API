using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.InformationTranslateDTOs
{
    public record InformationTranslateGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int InformationId { get; set; }
        public Language Language { get; set; }
    }
}
