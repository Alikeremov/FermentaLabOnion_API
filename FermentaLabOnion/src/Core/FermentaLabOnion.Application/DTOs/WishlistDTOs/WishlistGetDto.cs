using FermentaLabOnion.Application.DTOs.WishlistitemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.WishlistDTOs
{
    public class WishlistGetDto
    {
        public int WishlistId { get; set; }
        public List<WishlistItemDto> Items { get; set; } = new();
    }
}
