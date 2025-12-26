using FermentaLabOnion.Application.DTOs.QuestionTranslateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IQuestionTranslateService
    {
        Task<ICollection<QuestionTranslateGetDto>> GetAllAsync(int page, int take);
        Task<QuestionTranslateGetDto> GetAsync(int id);
        Task CreateAsync(QuestionTranslateCreateDto questionDto);
        Task UpdateAsync(QuestionTranslateUpdateDto questionDto, int id);
        Task DeleteAsync(int id);
    }
}
