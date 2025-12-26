using FermentaLabOnion.Application.DTOs.InformationTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IInformationTranslateService
    {
        Task<ICollection<InformationTranslateGetDto>> GetAllAsync(int page, int take);
        Task<InformationTranslateGetDto> GetAsync(int id);
        Task CreateAsync(InformationTranslateCreateDto informationDto);
        Task UpdateAsync(InformationTranslateUpdateDto informationDto, int id);
        Task DeleteAsync(int id);
    }
}
