using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.TagDTOs;
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
    public class TagService : ITagService
    {
        private readonly ITagRepo _repository;
        private readonly IMapper _mapper;
        private readonly ITagTranslateRepo _translateRepo;

        public TagService(ITagRepo repository, IMapper mapper, ITagTranslateRepo translateRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
        }

        public async Task<ICollection<TagGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<TagGetDto>>(tags);
        }
        public async Task<ICollection<TagGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<TagGetDto> tagItemDtos = _mapper.Map<ICollection<TagGetDto>>(tags);

            ICollection<TagTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var tagItemDto = tagItemDtos.FirstOrDefault(dto => dto.Id == translate.TagId);
                if (tagItemDto != null)
                {
                    tagItemDto.Name = translate.Name ?? tagItemDto.Name;
                }
            }
            return tagItemDtos;
        }
        public async Task<TagGetDto> GetAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new NotFoundException();
            return _mapper.Map<TagGetDto>(tag);
        }
        public async Task<TagGetDto> GetTranslatedAsync(int id, Language language)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new NotFoundException();
            TagGetDto tagDto = _mapper.Map<TagGetDto>(tag);
            TagTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.TagId == id,
                language: language);
            if (translate != null)
            {
                tagDto.Name = translate.Name ?? tagDto.Name;
            }
            else
            {
                tagDto.Name = "Default name";
            }
            return tagDto;
        }

        public async Task CreateAsync(TagCreateDto tagdto)
        {
            var result = await _repository.IsExistAsync(x => x.Name == tagdto.Name);
            if (result)
                throw new AlreadyExistException("This Name alredy exist");
            Tag tag = _mapper.Map<Tag>(tagdto);
            await _repository.AddAsync(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagUpdateDto tagdto, int id)
        {
            Tag existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            var result = await _repository.IsExistAsync(x => x.Name == tagdto.Name);
            if (result)
                throw new AlreadyExistException("This Name alredy exist");
            existed = _mapper.Map(tagdto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            Tag existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
