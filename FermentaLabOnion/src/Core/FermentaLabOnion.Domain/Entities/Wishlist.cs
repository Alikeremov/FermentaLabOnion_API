using FermentaLabOnion.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Domain.Entities
{
    public class Wishlist:BaseEntity
    {
        public string? AppUserId { get; set; }   
        public string? CookieId { get; set; }    

        public ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}
