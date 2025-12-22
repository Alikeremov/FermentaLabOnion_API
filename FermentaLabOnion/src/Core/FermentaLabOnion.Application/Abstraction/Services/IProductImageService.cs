using FermentaLabOnion.Application.DTOs.ProductImageDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IProductImageService
    {
        Task<ICollection<ProductImageGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<ProductImageGetDto>> GetAllByProductIdAsync(int page, int take,int productId);
        Task<ProductImageGetDto> GetAsync(int id);
        Task<ProductImageGetDto> GetByImageTypeAsync(int productId,ImageType imageType);
        Task CreateAsync(ProductImageCreateDto categoryDto);
        Task UpdateAsync(ProductImageUpdateDto categoryDto, int id);
        Task DeleteAsync(int id);
    }
}
