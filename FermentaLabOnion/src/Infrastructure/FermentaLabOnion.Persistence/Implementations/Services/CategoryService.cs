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
            ICollection<Category> informations = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<CategoryGetDto>>(informations);
        }
        public async Task<ICollection<CategoryGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<Category> informations = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<CategoryGetDto> informationItemDtos = _mapper.Map<ICollection<CategoryGetDto>>(informations);

            ICollection<CategoryTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var informationItemDto = informationItemDtos.FirstOrDefault(dto => dto.Id == translate.CategoryId);
                if (informationItemDto != null)
                {
                    informationItemDto.Name = translate.Name ?? informationItemDto.Name;
                }
            }
            return informationItemDtos;
        }
        public async Task<CategoryGetDto> GetAsync(int id)
        {
            Category information = await _repository.GetByIdAsync(id);
            if (information == null) throw new NotFoundException();
            return _mapper.Map<CategoryGetDto>(information);
        }
        public async Task<CategoryGetDto> GetTranslatedAsync(int id, Language language)
        {
            Category information = await _repository.GetByIdAsync(id);
            if (information == null) throw new NotFoundException();
            CategoryGetDto informationDto = _mapper.Map<CategoryGetDto>(information);
            CategoryTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.CategoryId == id,
                language: language);
            if (translate != null)
            {
                informationDto.Name = translate.Name ?? informationDto.Name;
            }
            else
            {
                informationDto.Name = "Default name";  
            }
            return informationDto;
        }

        public async Task CreateAsync(CategoryCreateDto informationdto)
        {
            var result = await _repository.IsExistAsync(x => x.Name == informationdto.Name);
            if (result)
                throw new AlreadyExistException("This Name alredy exist");
            Category information = _mapper.Map<Category>(informationdto);
            await _repository.AddAsync(information);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryUpdateDto informationdto, int id)
        {
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            var result = await _repository.IsExistAsync(x => x.Name == informationdto.Name);
            if (result)
                throw new AlreadyExistException("This Name alredy exist");
            existed = _mapper.Map(informationdto, existed);
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
