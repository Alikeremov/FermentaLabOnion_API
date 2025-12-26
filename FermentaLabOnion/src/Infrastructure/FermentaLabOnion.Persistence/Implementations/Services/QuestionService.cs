using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.QuestionDTOs;
using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Domain.Enums;
using FermentaLabOnion.Persistence.Utilites.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Implementations.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _repository;
        private readonly IMapper _mapper;
        private readonly IQuestionTranslateRepo _translateRepo;

        public QuestionService(IQuestionRepo repository, IMapper mapper, IQuestionTranslateRepo translateRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
        }

        public async Task<ICollection<QuestionGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<Question> questions = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<QuestionGetDto>>(questions);
        }
        public async Task<ICollection<QuestionGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<Question> questions = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<QuestionGetDto> questionItemDtos = _mapper.Map<ICollection<QuestionGetDto>>(questions);

            ICollection<QuestionTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var questionItemDto = questionItemDtos.FirstOrDefault(dto => dto.Id == translate.QuestionId);
                if (questionItemDto != null)
                {
                    questionItemDto.Title = translate.Title ?? questionItemDto.Title;
                    questionItemDto.Description = translate.Description ?? questionItemDto.Description;
                }
            }
            return questionItemDtos;
        }
        public async Task<QuestionGetDto> GetAsync(int id)
        {
            Question question = await _repository.GetByIdAsync(id);
            if (question == null) throw new NotFoundException();
            return _mapper.Map<QuestionGetDto>(question);
        }
        public async Task<QuestionGetDto> GetTranslatedAsync(int id, Language language)
        {
            Question question = await _repository.GetByIdAsync(id);
            if (question == null) throw new NotFoundException();
            QuestionGetDto questionDto = _mapper.Map<QuestionGetDto>(question);
            QuestionTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.QuestionId == id,
                language: language);
            if (translate != null)
            {
                questionDto.Title = translate.Title ?? questionDto.Title;
                questionDto.Description=translate.Description ?? questionDto.Description;
            }
            else
            {
                questionDto.Title = "Default name";
            }
            return questionDto;
        }

        public async Task CreateAsync(QuestionCreateDto questiondto)
        {
            Question question = _mapper.Map<Question>(questiondto);
            await _repository.AddAsync(question);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuestionUpdateDto questiondto, int id)
        {
            Question existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            existed = _mapper.Map(questiondto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            Question existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
