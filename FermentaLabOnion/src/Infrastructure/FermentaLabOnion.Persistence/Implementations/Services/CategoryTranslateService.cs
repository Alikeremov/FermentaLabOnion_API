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
        private readonly ICategoryRepo _ourValueRepo;

        public CategoryTranslateService(ICategoryTranslateRepo repository, IMapper mapper, ICategoryRepo ourValueRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _ourValueRepo = ourValueRepo;
        }
        public async Task<ICollection<CategoryTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<CategoryTranslate> ourValues = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<CategoryTranslateGetDto>>(ourValues);
        }

        public async Task<CategoryTranslateGetDto> GetAsync(int id)
        {
            CategoryTranslate ourValue = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryTranslateGetDto>(ourValue);
        }

        public async Task CreateAsync(CategoryTranslateCreateDto ourValueDto)
        {
            if (!await _ourValueRepo.IsExistAsync(x => x.Id == ourValueDto.CategoryId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), ourValueDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.CategoryId == ourValueDto.CategoryId && x.Language == ourValueDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            CategoryTranslate ourValueTranslate = _mapper.Map<CategoryTranslate>(ourValueDto);
            await _repository.AddAsync(ourValueTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryTranslateUpdateDto ourValueDto, int id)
        {
            CategoryTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _ourValueRepo.IsExistAsync(x => x.Id == ourValueDto.CategoryId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), ourValueDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(ourValueDto, existed);
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
