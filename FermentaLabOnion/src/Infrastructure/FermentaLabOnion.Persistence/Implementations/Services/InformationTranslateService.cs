using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.InformationTranslateDTOs;
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
    public class InformationTranslateService : IInformationTranslateService
    {
        private readonly IInformationTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly IInformationRepo _informationRepo;

        public InformationTranslateService(IInformationTranslateRepo repository, IMapper mapper, IInformationRepo informationRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _informationRepo = informationRepo;
        }
        public async Task<ICollection<InformationTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<InformationTranslate> informations = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<InformationTranslateGetDto>>(informations);
        }

        public async Task<InformationTranslateGetDto> GetAsync(int id)
        {
            InformationTranslate information = await _repository.GetByIdAsync(id);
            return _mapper.Map<InformationTranslateGetDto>(information);
        }

        public async Task CreateAsync(InformationTranslateCreateDto informationDto)
        {
            if (!await _informationRepo.IsExistAsync(x => x.Id == informationDto.InformationId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), informationDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.InformationId == informationDto.InformationId && x.Language == informationDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            InformationTranslate informationTranslate = _mapper.Map<InformationTranslate>(informationDto);
            await _repository.AddAsync(informationTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(InformationTranslateUpdateDto informationDto, int id)
        {
            InformationTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _informationRepo.IsExistAsync(x => x.Id == informationDto.InformationId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), informationDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(informationDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            InformationTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
