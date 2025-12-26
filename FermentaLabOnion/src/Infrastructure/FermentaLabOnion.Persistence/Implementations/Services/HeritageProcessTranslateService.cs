using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.HeritageProcessTranslateDTOs;
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
    public class HeritageProcessTranslateService : IHeritageProcessTranslateService
    {
        private readonly IHeritageProcessTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHeritageProcessRepo _heritageProcessRepo;

        public HeritageProcessTranslateService(IHeritageProcessTranslateRepo repository, IMapper mapper, IHeritageProcessRepo heritageProcessRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _heritageProcessRepo = heritageProcessRepo;
        }
        public async Task<ICollection<HeritageProcessTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<HeritageProcessTranslate> heritageProcesss = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<HeritageProcessTranslateGetDto>>(heritageProcesss);
        }

        public async Task<HeritageProcessTranslateGetDto> GetAsync(int id)
        {
            HeritageProcessTranslate heritageProcess = await _repository.GetByIdAsync(id);
            return _mapper.Map<HeritageProcessTranslateGetDto>(heritageProcess);
        }

        public async Task CreateAsync(HeritageProcessTranslateCreateDto heritageProcessDto)
        {
            if (!await _heritageProcessRepo.IsExistAsync(x => x.Id == heritageProcessDto.HeritageProcessId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), heritageProcessDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.HeritageProcessId == heritageProcessDto.HeritageProcessId && x.Language == heritageProcessDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            HeritageProcessTranslate heritageProcessTranslate = _mapper.Map<HeritageProcessTranslate>(heritageProcessDto);
            await _repository.AddAsync(heritageProcessTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(HeritageProcessTranslateUpdateDto heritageProcessDto, int id)
        {
            HeritageProcessTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _heritageProcessRepo.IsExistAsync(x => x.Id == heritageProcessDto.HeritageProcessId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), heritageProcessDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(heritageProcessDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            HeritageProcessTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
