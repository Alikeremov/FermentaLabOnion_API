using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ProductImageDTOs;
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
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductImageService(IProductImageRepo repository, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _repository = repository;

            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<ICollection<ProductImageGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<ProductImage> productImages = await _repository.GetAllWhere(
                skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ProductImageGetDto>>(productImages);
        }
        
        public async Task<ProductImageGetDto> GetAsync(int id)
        {
            ProductImage productImage = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductImageGetDto>(productImage);
        }
        
        public async Task<ICollection<ProductImageGetDto>> GetAllByProductIdAsync(int page, int take, int productId)
        {
            ICollection<ProductImage> productImages = await _repository.GetAllWhere(x=>x.ProductId==productId,
                 skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<ProductImageGetDto>>(productImages);
        }

        public async Task<ProductImageGetDto> GetByImageTypeAsync(int productId, ImageType imageType)
        {
            ProductImage productImage = await _repository.GetByExpressionAsync(x=>x.ProductId==productId && x.ImageType==imageType);
            return _mapper.Map<ProductImageGetDto>(productImage);
        }
    
        public async Task CreateAsync(ProductImageCreateDto productImagedto)
        {
            productImagedto.Image.ValidateImage();
            ProductImage productImage = _mapper.Map<ProductImage>(productImagedto);
            productImage.Url = await _cloudinaryService.FileCreateAsync(productImagedto.Image);
            await _repository.AddAsync(productImage);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductImageUpdateDto productImagedto, int id)
        {
            ProductImage existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            existed = _mapper.Map(productImagedto, existed);

            if (productImagedto.NewImage != null)
            {
                productImagedto.NewImage.ValidateImage();
                var result = await _cloudinaryService.FileDeleteAsync(existed.Url);
                if (result == false) throw new UnDeleteException();
                existed.Url = await _cloudinaryService.FileCreateAsync(productImagedto.NewImage);
            }
            await _repository.UpdateAsync(existed);
        }
        public async Task DeleteAsync(int id)
        {
            ProductImage existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException();
            var result = await _cloudinaryService.FileDeleteAsync(existed.Url);
            if (result == false) throw new UnDeleteException();
            await _repository.DeleteAsync(existed);
        }

    }

}
