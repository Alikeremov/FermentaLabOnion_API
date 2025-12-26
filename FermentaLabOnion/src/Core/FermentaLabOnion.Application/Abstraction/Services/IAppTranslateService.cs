using FermentaLabOnion.Application.DTOs.AppTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IAppTranslateService
    {
        Task<ICollection<AppTranslateGetDto>> GetAllAsync(int page, int take);
        Task<AppTranslateGetDto> GetAsync(int id);
        Task CreateAsync(AppTranslateCreateDto appDto);
        Task UpdateAsync(AppTranslateUpdateDto appDto, int id);
        Task DeleteAsync(int id);
    }
}
