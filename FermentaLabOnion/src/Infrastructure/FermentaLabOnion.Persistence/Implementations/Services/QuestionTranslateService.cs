using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.QuestionTranslateDTOs;
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
    public class QuestionTranslateService : IQuestionTranslateService
    {
        private readonly IQuestionTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly IQuestionRepo _questionRepo;

        public QuestionTranslateService(IQuestionTranslateRepo repository, IMapper mapper, IQuestionRepo questionRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _questionRepo = questionRepo;
        }
        public async Task<ICollection<QuestionTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<QuestionTranslate> questions = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<QuestionTranslateGetDto>>(questions);
        }

        public async Task<QuestionTranslateGetDto> GetAsync(int id)
        {
            QuestionTranslate question = await _repository.GetByIdAsync(id);
            return _mapper.Map<QuestionTranslateGetDto>(question);
        }

        public async Task CreateAsync(QuestionTranslateCreateDto questionDto)
        {
            if (!await _questionRepo.IsExistAsync(x => x.Id == questionDto.QuestionId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), questionDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.QuestionId == questionDto.QuestionId && x.Language == questionDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            QuestionTranslate questionTranslate = _mapper.Map<QuestionTranslate>(questionDto);
            await _repository.AddAsync(questionTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuestionTranslateUpdateDto questionDto, int id)
        {
            QuestionTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _questionRepo.IsExistAsync(x => x.Id == questionDto.QuestionId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), questionDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(questionDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            QuestionTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
