using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.HeritageProcessDTOs;
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
    public class HeritageProcessService : IHeritageProcessService
    {
        private readonly IHeritageProcessRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHeritageProcessTranslateRepo _translateRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public HeritageProcessService(IHeritageProcessRepo repository, IMapper mapper, IHeritageProcessTranslateRepo translateRepo, ICloudinaryService cloudinaryService)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ICollection<HeritageProcessGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<HeritageProcess> heritageProcesss = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<HeritageProcessGetDto>>(heritageProcesss);
        }
        public async Task<ICollection<HeritageProcessGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<HeritageProcess> heritageProcesss = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<HeritageProcessGetDto> heritageProcessItemDtos = _mapper.Map<ICollection<HeritageProcessGetDto>>(heritageProcesss);

            ICollection<HeritageProcessTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var heritageProcessItemDto = heritageProcessItemDtos.FirstOrDefault(dto => dto.Id == translate.HeritageProcessId);
                if (heritageProcessItemDto != null)
                {
                    heritageProcessItemDto.Title = translate.Title ?? heritageProcessItemDto.Title;
                }
            }
            return heritageProcessItemDtos;
        }
        public async Task<HeritageProcessGetDto> GetAsync(int id)
        {
            HeritageProcess heritageProcess = await _repository.GetByIdAsync(id);
            if (heritageProcess == null) throw new NotFoundException();
            return _mapper.Map<HeritageProcessGetDto>(heritageProcess);
        }
        public async Task<HeritageProcessGetDto> GetTranslatedAsync(int id, Language language)
        {
            HeritageProcess heritageProcess = await _repository.GetByIdAsync(id);
            if (heritageProcess == null) throw new NotFoundException();
            HeritageProcessGetDto heritageProcessDto = _mapper.Map<HeritageProcessGetDto>(heritageProcess);
            HeritageProcessTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.HeritageProcessId == id,
                language: language);
            if (translate != null)
            {
                heritageProcessDto.Title = translate.Title ?? heritageProcessDto.Title;
            }
            else
            {
                heritageProcessDto.Title = "Default name";
            }
            return heritageProcessDto;
        }

        public async Task CreateAsync(HeritageProcessCreateDto heritageProcessdto)
        {

            HeritageProcess heritageProcess = _mapper.Map<HeritageProcess>(heritageProcessdto);
            if (heritageProcessdto.BeforeImageUrl != null)
            {
                heritageProcessdto.BeforeImageUrl.ValidateImage();
                heritageProcess.BeforeImageUrl = await _cloudinaryService.FileCreateAsync(heritageProcessdto.BeforeImageUrl);
            }
            if (heritageProcessdto.AfterImageUrl != null)
            {
                heritageProcessdto.AfterImageUrl.ValidateImage();
                heritageProcess.AfterImageUrl = await _cloudinaryService.FileCreateAsync(heritageProcessdto.AfterImageUrl);
            }
            await _repository.AddAsync(heritageProcess);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(HeritageProcessUpdateDto heritageProcessdto, int id)
        {
            HeritageProcess existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            existed = _mapper.Map(heritageProcessdto, existed);
            if (heritageProcessdto.NewAfterImage != null)
            {
                var imageResult = true;
                heritageProcessdto.NewAfterImage.ValidateImage();
                if (existed.AfterImageUrl != null)
                    imageResult = await _cloudinaryService.FileDeleteAsync(existed.AfterImageUrl);
                if (!imageResult)
                    throw new UnDeleteException();
                existed.AfterImageUrl = await _cloudinaryService.FileCreateAsync(heritageProcessdto.NewAfterImage);
            }
            if (heritageProcessdto.NewBeforeImage != null)
            {
                var imageResult = true;
                heritageProcessdto.NewBeforeImage.ValidateImage();
                if (existed.BeforeImageUrl != null)
                    imageResult = await _cloudinaryService.FileDeleteAsync(existed.BeforeImageUrl);
                if (!imageResult)
                    throw new UnDeleteException();
                existed.BeforeImageUrl = await _cloudinaryService.FileCreateAsync(heritageProcessdto.NewBeforeImage);
            }
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            HeritageProcess existed = await _repository.GetByIdAsync(id);
            var result = true;
            if (existed == null) throw new NotFoundException();
            if (existed.BeforeImageUrl != null)
                result = await _cloudinaryService.FileDeleteAsync(existed.BeforeImageUrl);
            if (existed.AfterImageUrl != null)
                result = await _cloudinaryService.FileDeleteAsync(existed.AfterImageUrl);
            if (result == false) throw new UnDeleteException();
            await _repository.DeleteAsync(existed);
        }
    }
}
