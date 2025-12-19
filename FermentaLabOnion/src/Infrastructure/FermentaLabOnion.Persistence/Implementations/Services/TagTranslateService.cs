using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.TagTranslateDTOs;
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
    public class TagTranslateService : ITagTranslateService
    {
        private readonly ITagTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly ITagRepo _tagRepo;

        public TagTranslateService(ITagTranslateRepo repository, IMapper mapper, ITagRepo tagRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _tagRepo = tagRepo;
        }
        public async Task<ICollection<TagTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<TagTranslate> tags = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<TagTranslateGetDto>>(tags);
        }

        public async Task<TagTranslateGetDto> GetAsync(int id)
        {
            TagTranslate tag = await _repository.GetByIdAsync(id);
            return _mapper.Map<TagTranslateGetDto>(tag);
        }

        public async Task CreateAsync(TagTranslateCreateDto tagDto)
        {
            if (!await _tagRepo.IsExistAsync(x => x.Id == tagDto.TagId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), tagDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.TagId == tagDto.TagId && x.Language == tagDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            TagTranslate tagTranslate = _mapper.Map<TagTranslate>(tagDto);
            await _repository.AddAsync(tagTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagTranslateUpdateDto tagDto, int id)
        {
            TagTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _tagRepo.IsExistAsync(x => x.Id == tagDto.TagId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), tagDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(tagDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            TagTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
