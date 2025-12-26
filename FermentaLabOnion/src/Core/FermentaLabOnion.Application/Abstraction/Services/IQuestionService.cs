using FermentaLabOnion.Application.DTOs.QuestionDTOs;
using FermentaLabOnion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IQuestionService
    {
        Task<ICollection<QuestionGetDto>> GetAllAsync(int page, int take);
        Task<ICollection<QuestionGetDto>> GetAllTranslatedAsync(int page, Language language, int take);
        Task<QuestionGetDto> GetAsync(int id);
        Task<QuestionGetDto> GetTranslatedAsync(int id, Language language);
        Task CreateAsync(QuestionCreateDto questionDto);
        Task UpdateAsync(QuestionUpdateDto questionDto, int id);
        Task DeleteAsync(int id);
    }
}
