using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.CategoryDTOs;
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
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryTranslateRepo _translateRepo;

        public CategoryService(ICategoryRepo repository, IMapper mapper, ICategoryTranslateRepo translateRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
        }

        public async Task<ICollection<CategoryGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categorys = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<CategoryGetDto>>(categorys);
        }
        public async Task<ICollection<CategoryGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<Category> categorys = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<CategoryGetDto> categoryItemDtos = _mapper.Map<ICollection<CategoryGetDto>>(categorys);

            ICollection<CategoryTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var categoryItemDto = categoryItemDtos.FirstOrDefault(dto => dto.Id == translate.CategoryId);
                if (categoryItemDto != null)
                {
                    categoryItemDto.Name = translate.Name ?? categoryItemDto.Name;
                }
            }
            return categoryItemDtos;
        }
        public async Task<CategoryGetDto> GetAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new NotFoundException();
            return _mapper.Map<CategoryGetDto>(category);
        }
        public async Task<CategoryGetDto> GetTranslatedAsync(int id, Language language)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new NotFoundException();
            CategoryGetDto categoryDto = _mapper.Map<CategoryGetDto>(category);
            CategoryTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.CategoryId == id,
                language: language);
            if (translate != null)
            {
                categoryDto.Name = translate.Name ?? categoryDto.Name;
            }
            else
            {
                categoryDto.Name = "Default name";  
            }
            return categoryDto;
        }

        public async Task CreateAsync(CategoryCreateDto categorydto)
        {
            var result = await _repository.IsExistAsync(x => x.Name == categorydto.Name);
            if (result)
                throw new AlreadyExistException("This Name alredy exist");
            Category category = _mapper.Map<Category>(categorydto);
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryUpdateDto categorydto, int id)
        {
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            var result = await _repository.IsExistAsync(x => x.Name == categorydto.Name);
            if (result)
                throw new AlreadyExistException("This Name alredy exist");
            existed = _mapper.Map(categorydto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
