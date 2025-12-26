using FermentaLabOnion.Application.DTOs.HeritageProcessTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IHeritageProcessTranslateService
    {
        Task<ICollection<HeritageProcessTranslateGetDto>> GetAllAsync(int page, int take);
        Task<HeritageProcessTranslateGetDto> GetAsync(int id);
        Task CreateAsync(HeritageProcessTranslateCreateDto heritageProcessDto);
        Task UpdateAsync(HeritageProcessTranslateUpdateDto heritageProcessDto, int id);
        Task DeleteAsync(int id);
    }
}
