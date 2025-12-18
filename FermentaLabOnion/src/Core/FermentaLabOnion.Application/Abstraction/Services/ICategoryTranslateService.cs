using FermentaLabOnion.Application.DTOs.CategoryTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface ICategoryTranslateService
    {
        Task<ICollection<CategoryTranslateGetDto>> GetAllAsync(int page, int take);
        Task<CategoryTranslateGetDto> GetAsync(int id);
        Task CreateAsync(CategoryTranslateCreateDto aboutDto);
        Task UpdateAsync(CategoryTranslateUpdateDto aboutDto, int id);
        Task DeleteAsync(int id);
    }
}
