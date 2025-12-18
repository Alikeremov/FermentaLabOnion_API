using FermentaLabOnion.Application.DTOs.CategoryDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<CategoryGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<CategoryGetDto> GetAsync(int id);
        Task<CategoryGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(CategoryCreateDto aboutDto);
        Task UpdateAsync(CategoryUpdateDto aboutDto, int id);
        Task DeleteAsync(int id);
    }
}
