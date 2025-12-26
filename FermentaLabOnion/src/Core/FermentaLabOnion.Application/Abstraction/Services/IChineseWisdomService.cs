using FermentaLabOnion.Application.DTOs.ChineseWisdomDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IChineseWisdomService
    {
        Task<ICollection<ChineseWisdomGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<ChineseWisdomGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<ChineseWisdomGetDto> GetAsync(int id);
        Task<ChineseWisdomGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(ChineseWisdomCreateDto chineseWisdomDto);
        Task UpdateAsync(ChineseWisdomUpdateDto chineseWisdomDto, int id);
        Task DeleteAsync(int id);
    }
}
