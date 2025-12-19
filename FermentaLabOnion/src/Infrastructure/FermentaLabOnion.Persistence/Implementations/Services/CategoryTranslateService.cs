using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.CategoryTranslateDTOs;
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
    public class CategoryTranslateService : ICategoryTranslateService
    {
        private readonly ICategoryTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;

        public CategoryTranslateService(ICategoryTranslateRepo repository, IMapper mapper, ICategoryRepo categoryRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }
        public async Task<ICollection<CategoryTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<CategoryTranslate> categorys = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<CategoryTranslateGetDto>>(categorys);
        }

        public async Task<CategoryTranslateGetDto> GetAsync(int id)
        {
            CategoryTranslate category = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryTranslateGetDto>(category);
        }

        public async Task CreateAsync(CategoryTranslateCreateDto categoryDto)
        {
            if (!await _categoryRepo.IsExistAsync(x => x.Id == categoryDto.CategoryId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), categoryDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.CategoryId == categoryDto.CategoryId && x.Language == categoryDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            CategoryTranslate categoryTranslate = _mapper.Map<CategoryTranslate>(categoryDto);
            await _repository.AddAsync(categoryTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryTranslateUpdateDto categoryDto, int id)
        {
            CategoryTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _categoryRepo.IsExistAsync(x => x.Id == categoryDto.CategoryId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), categoryDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(categoryDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            CategoryTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
