using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.QuestionTranslateDTOs
{
    public record QuestionTranslateGetDto
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int QuestionId { get; set; }
        public Language Language { get; set; }
    }
}
