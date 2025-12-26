using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.AppTranslateDTOs;
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
    public class AppTranslateService : IAppTranslateService
    {
        private readonly IAppTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAppRepo _appRepo;

        public AppTranslateService(IAppTranslateRepo repository, IMapper mapper, IAppRepo appRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _appRepo = appRepo;
        }
        public async Task<ICollection<AppTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<AppTranslate> apps = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<AppTranslateGetDto>>(apps);
        }

        public async Task<AppTranslateGetDto> GetAsync(int id)
        {
            AppTranslate app = await _repository.GetByIdAsync(id);
            return _mapper.Map<AppTranslateGetDto>(app);
        }

        public async Task CreateAsync(AppTranslateCreateDto appDto)
        {
            if (!await _appRepo.IsExistAsync(x => x.Id == appDto.ApplicationId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), appDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.ApplicationId == appDto.ApplicationId && x.Language == appDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            AppTranslate appTranslate = _mapper.Map<AppTranslate>(appDto);
            await _repository.AddAsync(appTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppTranslateUpdateDto appDto, int id)
        {
            AppTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _appRepo.IsExistAsync(x => x.Id == appDto.ApplicationId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), appDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(appDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            AppTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
