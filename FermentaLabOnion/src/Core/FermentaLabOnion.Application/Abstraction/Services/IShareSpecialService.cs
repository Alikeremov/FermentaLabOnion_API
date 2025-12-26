using FermentaLabOnion.Application.DTOs.ShareSpecialDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IShareSpecialService
    {
        Task<ICollection<ShareSpecialGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<ShareSpecialGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<ShareSpecialGetDto> GetAsync(int id);
        Task<ShareSpecialGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(ShareSpecialCreateDto shareSpecialDto);
        Task UpdateAsync(ShareSpecialUpdateDto shareSpecialDto, int id);
        Task DeleteAsync(int id);
    }
}
