using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ShareSpecialDTOs;
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
    public class ShareSpecialService : IShareSpecialService
    {
        private readonly IShareSpecialRepo _repository;
        private readonly IMapper _mapper;
        private readonly IShareSpecialTranslateRepo _translateRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public ShareSpecialService(IShareSpecialRepo repository, IMapper mapper, IShareSpecialTranslateRepo translateRepo, ICloudinaryService cloudinaryService)
        {
            _repository = repository;
            _mapper = mapper;
            _translateRepo = translateRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ICollection<ShareSpecialGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<ShareSpecial> shareSpecials = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ShareSpecialGetDto>>(shareSpecials);
        }
        public async Task<ICollection<ShareSpecialGetDto>> GetAllTranslatedAsync(int page,
            Language language, int take)
        {
            ICollection<ShareSpecial> shareSpecials = await _repository.GetAllWhere(
            skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<ShareSpecialGetDto> shareSpecialItemDtos = _mapper.Map<ICollection<ShareSpecialGetDto>>(shareSpecials);

            ICollection<ShareSpecialTranslate> translates = await _translateRepo
                .GetAllWhereTranslated(language: language, skip: (page - 1) * take, take: take)
                .ToListAsync();

            foreach (var translate in translates)
            {
                var shareSpecialItemDto = shareSpecialItemDtos.FirstOrDefault(dto => dto.Id == translate.ShareSpecialId);
                if (shareSpecialItemDto != null)
                {
                    shareSpecialItemDto.Title = translate.Title ?? shareSpecialItemDto.Title;
                    shareSpecialItemDto.Subtitle = translate.Subtitle ?? shareSpecialItemDto.Subtitle;
                }
            }
            return shareSpecialItemDtos;
        }
        public async Task<ShareSpecialGetDto> GetAsync(int id)
        {
            ShareSpecial shareSpecial = await _repository.GetByIdAsync(id);
            if (shareSpecial == null) throw new NotFoundException();
            return _mapper.Map<ShareSpecialGetDto>(shareSpecial);
        }
        public async Task<ShareSpecialGetDto> GetTranslatedAsync(int id, Language language)
        {
            ShareSpecial shareSpecial = await _repository.GetByIdAsync(id);
            if (shareSpecial == null) throw new NotFoundException();
            ShareSpecialGetDto shareSpecialDto = _mapper.Map<ShareSpecialGetDto>(shareSpecial);
            ShareSpecialTranslate translate = await _translateRepo.GetByExpressionTranslatedAsync(
                x => x.ShareSpecialId == id,
                language: language);
            if (translate != null)
            {
                shareSpecialDto.Title = translate.Title ?? shareSpecialDto.Title;
                shareSpecialDto.Subtitle = translate.Subtitle ?? shareSpecialDto.Subtitle;
            }
            else
            {
                shareSpecialDto.Title = "Default name";
            }
            return shareSpecialDto;
        }

        public async Task CreateAsync(ShareSpecialCreateDto shareSpecialdto)
        {

            ShareSpecial shareSpecial = _mapper.Map<ShareSpecial>(shareSpecialdto);
            if (shareSpecialdto.Image != null)
            {
                shareSpecialdto.Image.ValidateImage();
                shareSpecial.Image = await _cloudinaryService.FileCreateAsync(shareSpecialdto.Image);
            }
            await _repository.AddAsync(shareSpecial);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShareSpecialUpdateDto shareSpecialdto, int id)
        {
            ShareSpecial existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            existed = _mapper.Map(shareSpecialdto, existed);
            if (shareSpecialdto.NewImage != null)
            {
                var imageResult = true;
                shareSpecialdto.NewImage.ValidateImage();
                if (existed.Image != null)
                    imageResult = await _cloudinaryService.FileDeleteAsync(existed.Image);
                if (!imageResult)
                    throw new UnDeleteException();
                existed.Image = await _cloudinaryService.FileCreateAsync(shareSpecialdto.NewImage);
            }
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            ShareSpecial existed = await _repository.GetByIdAsync(id);
            var result = true;
            if (existed == null) throw new NotFoundException();
            if (existed.Image != null)
                result = await _cloudinaryService.FileDeleteAsync(existed.Image);
            if (result == false) throw new UnDeleteException();
            await _repository.DeleteAsync(existed);
        }
    }
}
