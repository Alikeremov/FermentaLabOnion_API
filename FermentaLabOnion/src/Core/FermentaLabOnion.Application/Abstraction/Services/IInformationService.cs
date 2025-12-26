using FermentaLabOnion.Application.DTOs.InformationDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IInformationService
    {
        Task<ICollection<InformationGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<InformationGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<InformationGetDto> GetAsync(int id);
        Task<InformationGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(InformationCreateDto informationDto);
        Task UpdateAsync(InformationUpdateDto informationDto, int id);
        Task DeleteAsync(int id);
    }
}
