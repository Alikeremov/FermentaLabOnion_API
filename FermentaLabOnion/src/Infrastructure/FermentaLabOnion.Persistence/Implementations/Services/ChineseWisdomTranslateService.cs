using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ChineseWisdomTranslateDTOs;
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
    public class ChineseWisdomTranslateService : IChineseWisdomTranslateService
    {
        private readonly IChineseWisdomTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly IChineseWisdomRepo _chineseWisdomRepo;

        public ChineseWisdomTranslateService(IChineseWisdomTranslateRepo repository, IMapper mapper, IChineseWisdomRepo chineseWisdomRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _chineseWisdomRepo = chineseWisdomRepo;
        }
        public async Task<ICollection<ChineseWisdomTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<ChineseWisdomTranslate> chineseWisdoms = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ChineseWisdomTranslateGetDto>>(chineseWisdoms);
        }

        public async Task<ChineseWisdomTranslateGetDto> GetAsync(int id)
        {
            ChineseWisdomTranslate chineseWisdom = await _repository.GetByIdAsync(id);
            return _mapper.Map<ChineseWisdomTranslateGetDto>(chineseWisdom);
        }

        public async Task CreateAsync(ChineseWisdomTranslateCreateDto chineseWisdomDto)
        {
            if (!await _chineseWisdomRepo.IsExistAsync(x => x.Id == chineseWisdomDto.ChineseWisdomId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), chineseWisdomDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.ChineseWisdomId == chineseWisdomDto.ChineseWisdomId && x.Language == chineseWisdomDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            ChineseWisdomTranslate chineseWisdomTranslate = _mapper.Map<ChineseWisdomTranslate>(chineseWisdomDto);
            await _repository.AddAsync(chineseWisdomTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChineseWisdomTranslateUpdateDto chineseWisdomDto, int id)
        {
            ChineseWisdomTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _chineseWisdomRepo.IsExistAsync(x => x.Id == chineseWisdomDto.ChineseWisdomId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), chineseWisdomDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(chineseWisdomDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            ChineseWisdomTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
