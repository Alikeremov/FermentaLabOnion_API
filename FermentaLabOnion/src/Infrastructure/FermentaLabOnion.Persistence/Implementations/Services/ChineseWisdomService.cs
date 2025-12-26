using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ChineseWisdomDTOs;
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
    public class ChineseWisdomService : IChineseWisdomService
    {
        private readonly IChineseWisdomRepo _repository;
        private readonly IMapper _mapper;
        private readonly IChineseWisdomTranslateRepo _translateRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public ChineseWisdomService(IChineseWisdomRepo repository, IMapper mapper, IChineseWisdomTranslateRepo translateRepo, ICloudinaryService cloudinaryService)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ICollection<ChineseWisdomGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<ChineseWisdom> chineseWisdoms = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ChineseWisdomGetDto>>(chineseWisdoms);
        }
        public async Task<ICollection<ChineseWisdomGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<ChineseWisdom> chineseWisdoms = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<ChineseWisdomGetDto> chineseWisdomItemDtos = _mapper.Map<ICollection<ChineseWisdomGetDto>>(chineseWisdoms);

            ICollection<ChineseWisdomTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var chineseWisdomItemDto = chineseWisdomItemDtos.FirstOrDefault(dto => dto.Id == translate.ChineseWisdomId);
                if (chineseWisdomItemDto != null)
                {
                    chineseWisdomItemDto.Title = translate.Title ?? chineseWisdomItemDto.Title;
                    chineseWisdomItemDto.Description = translate.Description ?? chineseWisdomItemDto.Description;
                }
            }
            return chineseWisdomItemDtos;
        }
        public async Task<ChineseWisdomGetDto> GetAsync(int id)
        {
            ChineseWisdom chineseWisdom = await _repository.GetByIdAsync(id);
            if (chineseWisdom == null) throw new NotFoundException();
            return _mapper.Map<ChineseWisdomGetDto>(chineseWisdom);
        }
        public async Task<ChineseWisdomGetDto> GetTranslatedAsync(int id, Language language)
        {
            ChineseWisdom chineseWisdom = await _repository.GetByIdAsync(id);
            if (chineseWisdom == null) throw new NotFoundException();
            ChineseWisdomGetDto chineseWisdomDto = _mapper.Map<ChineseWisdomGetDto>(chineseWisdom);
            ChineseWisdomTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.ChineseWisdomId == id,
                language: language);
            if (translate != null)
            {
                chineseWisdomDto.Title = translate.Title ?? chineseWisdomDto.Title;
                chineseWisdomDto.Description = translate.Description ?? chineseWisdomDto.Description;
            }
            else
            {
                chineseWisdomDto.Title = "Default name";
            }
            return chineseWisdomDto;
        }

        public async Task CreateAsync(ChineseWisdomCreateDto chineseWisdomdto)
        {

            ChineseWisdom chineseWisdom = _mapper.Map<ChineseWisdom>(chineseWisdomdto);
            if (chineseWisdomdto.Image != null)
            {
                chineseWisdomdto.Image.ValidateImage();
                chineseWisdom.Image = await _cloudinaryService.FileCreateAsync(chineseWisdomdto.Image);
            }
            await _repository.AddAsync(chineseWisdom);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChineseWisdomUpdateDto chineseWisdomdto, int id)
        {
            ChineseWisdom existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            existed = _mapper.Map(chineseWisdomdto, existed);
            if (chineseWisdomdto.NewImage != null)
            {
                var imageResult = true;
                chineseWisdomdto.NewImage.ValidateImage();
                if (existed.Image != null)
                    imageResult = await _cloudinaryService.FileDeleteAsync(existed.Image);
                if (!imageResult)
                    throw new UnDeleteException();
                existed.Image = await _cloudinaryService.FileCreateAsync(chineseWisdomdto.NewImage);
            }
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            ChineseWisdom existed = await _repository.GetByIdAsync(id);
            var result = true;
            if (existed == null) throw new NotFoundException();
            if (existed.Image != null)
                result = await _cloudinaryService.FileDeleteAsync(existed.Image);
            if (result == false) throw new UnDeleteException();
            await _repository.DeleteAsync(existed);
        }
    }
}
