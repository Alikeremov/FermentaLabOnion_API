using FermentaLabOnion.Application.DTOs.TagDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface ITagService
    {
        Task<ICollection<TagGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<TagGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<TagGetDto> GetAsync(int id);
        Task<TagGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(TagCreateDto tagDto);
        Task UpdateAsync(TagUpdateDto tagDto, int id);
        Task DeleteAsync(int id);
    }
}
