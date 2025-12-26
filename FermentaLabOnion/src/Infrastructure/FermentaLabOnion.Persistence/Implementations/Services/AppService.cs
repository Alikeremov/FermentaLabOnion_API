using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.AppDTOs;
using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Domain.Enums;
using FermentaLabOnion.Persistence.Utilites.Exceptions.Common;
using FermentaLabOnion.Persistence.Utilites.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Implementations.Services
{
    public class AppService : IAppService
    {
        private readonly IAppRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAppTranslateRepo _translateRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public AppService(IAppRepo repository, IMapper mapper, IAppTranslateRepo translateRepo, ICloudinaryService cloudinaryService)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ICollection<AppGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<App> apps = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<AppGetDto>>(apps);
        }
        public async Task<ICollection<AppGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<App> apps = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<AppGetDto> appItemDtos = _mapper.Map<ICollection<AppGetDto>>(apps);

            ICollection<AppTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var appItemDto = appItemDtos.FirstOrDefault(dto => dto.Id == translate.ApplicationId);
                if (appItemDto != null)
                {
                    appItemDto.Title = translate.Title ?? appItemDto.Title;
                    appItemDto.Description = translate.Description ?? appItemDto.Description;
                }
            }
            return appItemDtos;
        }
        public async Task<AppGetDto> GetAsync(int id)
        {
            App app = await _repository.GetByIdAsync(id);
            if (app == null) throw new NotFoundException();
            return _mapper.Map<AppGetDto>(app);
        }
        public async Task<AppGetDto> GetTranslatedAsync(int id, Language language)
        {
            App app = await _repository.GetByIdAsync(id);
            if (app == null) throw new NotFoundException();
            AppGetDto appDto = _mapper.Map<AppGetDto>(app);
            AppTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.ApplicationId == id,
                language: language);
            if (translate != null)
            {
                appDto.Title = translate.Title ?? appDto.Title;
                appDto.Description = translate.Description ?? appDto.Description;
            }
            else
            {
                appDto.Title = "Default Title";
            }
            return appDto;
        }

        public async Task CreateAsync(AppCreateDto appdto)
        {
            App app = _mapper.Map<App>(appdto);
            if (appdto.Image != null)
            {
                appdto.Image.ValidateImage();
                app.Image = await _cloudinaryService.FileCreateAsync(appdto.Image);
            }
            await _repository.AddAsync(app);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppUpdateDto appdto, int id)
        {
            App existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            existed = _mapper.Map(appdto, existed);
            if (appdto.NewImage != null)
            {
                var imageResult = true;
                appdto.NewImage.ValidateImage();
                if (existed.Image != null)
                    imageResult = await _cloudinaryService.FileDeleteAsync(existed.Image);
                if (!imageResult)
                    throw new UnDeleteException();
                existed.Image = await _cloudinaryService.FileCreateAsync(appdto.NewImage);
            }
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            App existed = await _repository.GetByIdAsync(id);
            var result = true;
            if (existed == null) throw new NotFoundException();
            if (existed.Image != null)
                result = await _cloudinaryService.FileDeleteAsync(existed.Image);
            if (result == false) throw new UnDeleteException();
            await _repository.DeleteAsync(existed);
        }
    }
}
