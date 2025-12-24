using FermentaLabOnion.Application.DTOs.WishlistDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IWishlistService
    {
        Task AddAsync(int productId);
        Task RemoveAsync(int productId);
        Task<WishlistGetDto> GetAsync();
    }
}
