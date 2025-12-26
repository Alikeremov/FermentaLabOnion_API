using FermentaLabOnion.Application.DTOs.HeritageProcessDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IHeritageProcessService
    {
        Task<ICollection<HeritageProcessGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<HeritageProcessGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<HeritageProcessGetDto> GetAsync(int id);
        Task<HeritageProcessGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(HeritageProcessCreateDto heritageProcessDto);
        Task UpdateAsync(HeritageProcessUpdateDto heritageProcessDto, int id);
        Task DeleteAsync(int id);
    }
}
