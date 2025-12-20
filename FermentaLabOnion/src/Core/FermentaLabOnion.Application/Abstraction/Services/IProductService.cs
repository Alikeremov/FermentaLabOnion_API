using FermentaLabOnion.Application.DTOs.ProductDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<ProductGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<ProductGetDto> GetAsync(int id);
        Task<ProductGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(ProductCreateDto productDto);
        Task UpdateAsync(ProductUpdateDto productDto, int id);
        Task DeleteAsync(int id);
    }
}
