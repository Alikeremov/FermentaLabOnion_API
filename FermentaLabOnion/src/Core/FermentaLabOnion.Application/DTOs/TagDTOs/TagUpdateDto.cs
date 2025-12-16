using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.TagDTOs
{
    public record TagUpdateDto
    {
        public string Name { get; set; } = null!;

    }
}
