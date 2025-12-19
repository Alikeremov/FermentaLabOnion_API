using FermentaLabOnion.Application.DTOs.TagTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface ITagTranslateService
    {
        Task<ICollection<TagTranslateGetDto>> GetAllAsync(int page, int take);
        Task<TagTranslateGetDto> GetAsync(int id);
        Task CreateAsync(TagTranslateCreateDto tagDto);
        Task UpdateAsync(TagTranslateUpdateDto tagDto, int id);
        Task DeleteAsync(int id);
    }
}
