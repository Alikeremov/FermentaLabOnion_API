using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ReviewDTOs
{
    public record ReviewGetDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string AppUserId { get; set; } = null!;
        public string? UserName { get; set; }
        public string? UserImage { get; set; } 

        public string Description { get; set; } = null!;
        public int Rate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
