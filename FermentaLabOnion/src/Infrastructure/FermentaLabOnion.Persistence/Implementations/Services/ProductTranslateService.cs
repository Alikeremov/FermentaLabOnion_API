using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ProductTranslateDTOs;
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
    public class ProductTranslateService : IProductTranslateService
    {
        private readonly IProductTranslateRepo _repository;
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;

        public ProductTranslateService(IProductTranslateRepo repository, IMapper mapper, IProductRepo productRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepo = productRepo;
        }
        public async Task<ICollection<ProductTranslateGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<ProductTranslate> products = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ProductTranslateGetDto>>(products);
        }

        public async Task<ProductTranslateGetDto> GetAsync(int id)
        {
            ProductTranslate product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductTranslateGetDto>(product);
        }

        public async Task CreateAsync(ProductTranslateCreateDto productDto)
        {
            if (!await _productRepo.IsExistAsync(x => x.Id == productDto.ProductId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), productDto.Language);
            if (!isvalid) throw new BadRequestException();
            bool translateExists = await _repository.IsExistAsync(x => x.ProductId == productDto.ProductId && x.Language == productDto.Language);
            if (translateExists)
                throw new BadRequestException("A translation for this language already exists.");
            ProductTranslate productTranslate = _mapper.Map<ProductTranslate>(productDto);
            await _repository.AddAsync(productTranslate);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductTranslateUpdateDto productDto, int id)
        {
            ProductTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            if (!await _productRepo.IsExistAsync(x => x.Id == productDto.ProductId))
                throw new NotFoundException();
            bool isvalid = Enum.IsDefined(typeof(Language), productDto.Language);
            if (!isvalid) throw new BadRequestException();
            existed = _mapper.Map(productDto, existed);
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            ProductTranslate existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            await _repository.DeleteAsync(existed);
        }
    }
}
