using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ShareSpecialTranslateDTOs;
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
    public class ShareSpecialTranslateService : IShareSpecialTranslateService
    {
        private readonly IShareSpecialTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly IShareSpecialRepo _shareSpecialRepo;

        public ShareSpecialTranslateService(IShareSpecialTranslateRepo repository, IMapper mapper, IShareSpecialRepo shareSpecialRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _shareSpecialRepo = shareSpecialRepo;
        }
        public async Task<ICollection<ShareSpecialTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<ShareSpecialTranslate> shareSpecials = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ShareSpecialTranslateGetDto>>(shareSpecials);
        }

        public async Task<ShareSpecialTranslateGetDto> GetAsync(int id)
        {
            ShareSpecialTranslate shareSpecial = await _repository.GetByIdAsync(id);
            return _mapper.Map<ShareSpecialTranslateGetDto>(shareSpecial);
        }

        public async Task CreateAsync(ShareSpecialTranslateCreateDto shareSpecialDto)
        {
            if (!await _shareSpecialRepo.IsExistAsync(x => x.Id == shareSpecialDto.ShareSpecialId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), shareSpecialDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.ShareSpecialId == shareSpecialDto.ShareSpecialId && x.Language == shareSpecialDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            ShareSpecialTranslate shareSpecialTranslate = _mapper.Map<ShareSpecialTranslate>(shareSpecialDto);
            await _repository.AddAsync(shareSpecialTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShareSpecialTranslateUpdateDto shareSpecialDto, int id)
        {
            ShareSpecialTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _shareSpecialRepo.IsExistAsync(x => x.Id == shareSpecialDto.ShareSpecialId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), shareSpecialDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(shareSpecialDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            ShareSpecialTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
