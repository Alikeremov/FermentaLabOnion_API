using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ShareSpecialTranslateDTOs
{
    public record ShareSpecialTranslateUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Subtitle { get; set; } = null!;
        public int ShareSpecialId { get; set; }
        public Language Language { get; set; }
    }
}
