using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.HeritageProcessTranslateDTOs
{
    public record HeritageProcessTranslateGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string BeforeLabel { get; set; } = null!;
        public string AfterLabel { get; set; } = null!;
        public int HeritageProcessId { get; set; }
        public Language Language { get; set; }
    }
}
