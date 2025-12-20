using FermentaLabOnion.Application.DTOs.ProductTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IProductTranslateService
    {
        Task<ICollection<ProductTranslateGetDto>> GetAllAsync(int page, int take);
        Task<ProductTranslateGetDto> GetAsync(int id);
        Task CreateAsync(ProductTranslateCreateDto categoryDto);
        Task UpdateAsync(ProductTranslateUpdateDto categoryDto, int id);
        Task DeleteAsync(int id);
    }
}
