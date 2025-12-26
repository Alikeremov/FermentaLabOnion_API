using FermentaLabOnion.Application.DTOs.ChineseWisdomTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IChineseWisdomTranslateService
    {
        Task<ICollection<ChineseWisdomTranslateGetDto>> GetAllAsync(int page, int take);
        Task<ChineseWisdomTranslateGetDto> GetAsync(int id);
        Task CreateAsync(ChineseWisdomTranslateCreateDto chineseWisdomDto);
        Task UpdateAsync(ChineseWisdomTranslateUpdateDto chineseWisdomDto, int id);
        Task DeleteAsync(int id);
    }
}
