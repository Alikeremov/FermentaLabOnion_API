using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.InformationDTOs;
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
    public class InformationService : IInformationService
    {
        private readonly IInformationRepo _repository;
        private readonly IMapper _mapper;
        private readonly IInformationTranslateRepo _translateRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public InformationService(IInformationRepo repository, IMapper mapper, IInformationTranslateRepo translateRepo, ICloudinaryService cloudinaryService)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ICollection<InformationGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<Information> informations = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<InformationGetDto>>(informations);
        }
        public async Task<ICollection<InformationGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<Information> informations = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<InformationGetDto> informationItemDtos = _mapper.Map<ICollection<InformationGetDto>>(informations);

            ICollection<InformationTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var informationItemDto = informationItemDtos.FirstOrDefault(dto => dto.Id == translate.InformationId);
                if (informationItemDto != null)
                {
                    informationItemDto.Title = translate.Title ?? informationItemDto.Title;
                }
            }
            return informationItemDtos;
        }
        public async Task<InformationGetDto> GetAsync(int id)
        {
            Information information = await _repository.GetByIdAsync(id);
            if (information == null) throw new NotFoundException();
            return _mapper.Map<InformationGetDto>(information);
        }
        public async Task<InformationGetDto> GetTranslatedAsync(int id, Language language)
        {
            Information information = await _repository.GetByIdAsync(id);
            if (information == null) throw new NotFoundException();
            InformationGetDto informationDto = _mapper.Map<InformationGetDto>(information);
            InformationTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.InformationId == id,
                language: language);
            if (translate != null)
            {
                informationDto.Title = translate.Title ?? informationDto.Title;
            }
            else
            {
                informationDto.Title = "Default name";
            }
            return informationDto;
        }

        public async Task CreateAsync(InformationCreateDto informationdto)
        {

            Information information = _mapper.Map<Information>(informationdto);
            if (informationdto.Image != null)
            {
                informationdto.Image.ValidateImage();
                information.Image = await _cloudinaryService.FileCreateAsync(informationdto.Image);
            }
            await _repository.AddAsync(information);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(InformationUpdateDto informationdto, int id)
        {
            Information existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            existed = _mapper.Map(informationdto, existed);
            if (informationdto.NewImage != null)
            {
                var imageResult = true;
                informationdto.NewImage.ValidateImage();
                if (existed.Image != null)
                    imageResult = await _cloudinaryService.FileDeleteAsync(existed.Image);
                if (!imageResult)
                    throw new UnDeleteException();
                existed.Image = await _cloudinaryService.FileCreateAsync(informationdto.NewImage);
            }
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            Information existed = await _repository.GetByIdAsync(id);
            var result = true;
            if (existed == null) throw new NotFoundException();
            if (existed.Image != null)
                result = await _cloudinaryService.FileDeleteAsync(existed.Image);
            if (result == false) throw new UnDeleteException();
            await _repository.DeleteAsync(existed);
        }
    }
}
