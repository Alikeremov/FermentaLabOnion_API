using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.WishlistitemDTOs
{
    public record WishlistItemDto(
    int ProductId,
    string? Name,
    decimal? Price,
    string? MainImageUrl,
    string? HoverImageUrl
);
}
