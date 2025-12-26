using FermentaLabOnion.Application.DTOs.AppDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IAppService
    {
        Task<ICollection<AppGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<AppGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<AppGetDto> GetAsync(int id);
        Task<AppGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(AppCreateDto appDto);
        Task UpdateAsync(AppUpdateDto appDto, int id);
        Task DeleteAsync(int id);
    }
}
