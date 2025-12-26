using FermentaLabOnion.Application.DTOs.ShareSpecialTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IShareSpecialTranslateService
    {
        Task<ICollection<ShareSpecialTranslateGetDto>> GetAllAsync(int page, int take);
        Task<ShareSpecialTranslateGetDto> GetAsync(int id);
        Task CreateAsync(ShareSpecialTranslateCreateDto shareSpecialDto);
        Task UpdateAsync(ShareSpecialTranslateUpdateDto shareSpecialDto, int id);
        Task DeleteAsync(int id);
    }
}
