using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ProductDTOs;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IProductTranslateRepo _translateRepo;
        private readonly ITagRepo _tagRepo;
        private readonly ICategoryRepo _categoryRepo;

        public ProductService(IProductRepo productRepo,IMapper mapper, IProductTranslateRepo translateRepo,ITagRepo tagRepo,ICategoryRepo categoryRepo)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _translateRepo = translateRepo;
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
        }
        public async Task<ICollection<ProductGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<Product> products = await _productRepo
                .GetAllWhere(skip: (page - 1) * take, take: take,includes: new[] { "ProductTags", "ProductTags.Tag" })
                .ToListAsync();
            return _mapper.Map<ICollection<ProductGetDto>>(products);
        }

        public async Task<ICollection<ProductGetDto>> GetAllTranslatedAsync(int page, Language language, int take)
        {
            ICollection<Product> products = await _productRepo
                .GetAllWhere(skip: (page - 1) * take, take: take, includes: new[] { "ProductTags", "ProductTags.Tag" })
                .ToListAsync();
            ICollection<ProductGetDto> productGetDtos = _mapper.Map<ICollection<ProductGetDto>>(products);

            ICollection<ProductTranslate> productTranslates = await _translateRepo
                .GetAllWhere(skip: (page - 1) * take, take: take)
                .ToListAsync();
            foreach(var productTranslate in productTranslates)
            {
                var productGetDto = productGetDtos.FirstOrDefault(productDto => productDto.Id == productTranslate.ProductId);
                if(productGetDto != null)
                {
                    productGetDto.Name = productTranslate.Name ?? productGetDto.Name;
                    productGetDto.Slug = productTranslate.Slug ?? productGetDto.Slug;
                    productGetDto.ShortDescription = productTranslate.ShortDescription ?? productGetDto.ShortDescription;
                    productGetDto.Description = productTranslate.Description ?? productGetDto.Description;
                    productGetDto.QuantityPerPackage = productTranslate.QuantityPerPackage ?? productGetDto.QuantityPerPackage;
                    productGetDto.Volume = productTranslate.Volume ?? productGetDto.Volume;
                    productGetDto.PackageType = productTranslate.PackageType ?? productGetDto.PackageType;
                    productGetDto.Ingredients = productTranslate.Ingredients ?? productGetDto.Ingredients;
                    productGetDto.Benefits = productTranslate.Benefits ?? productGetDto.Benefits;
                    productGetDto.UsageInstructions = productTranslate.UsageInstructions ?? productGetDto.UsageInstructions;
                    productGetDto.Warnings = productTranslate.Warnings ?? productGetDto.Warnings;
                    productGetDto.Brand = productTranslate.Brand ?? productGetDto.Brand;
                    productGetDto.CountryOfOrigin = productTranslate.CountryOfOrigin ?? productGetDto.CountryOfOrigin;
                    productGetDto.ShelfLife = productTranslate.ShelfLife ?? productGetDto.ShelfLife;
                }
            }
                return productGetDtos;
        }
        public async Task<ProductGetDto> GetAsync(int id)
        {
            Product product=await _productRepo.GetByIdAsync(id, includes: new[] { "ProductTags", "ProductTags.Tag" });
            if (product == null) throw new NotFoundException("Product Not Found");
            return _mapper.Map<ProductGetDto>(product);
        }

        public async Task<ProductGetDto> GetTranslatedAsync(int id, Language language)
        {
            Product product = await _productRepo.GetByIdAsync(id, includes: new[] { "ProductTags", "ProductTags.Tag" });
            if (product == null) throw new NotFoundException("Product Not Found");
            ProductGetDto productGetDto=_mapper.Map<ProductGetDto>(product);
            ProductTranslate productTranslate=await _translateRepo.GetByExpressionTranslatedAsync(
                x=>x.ProductId==id
                ,language:language);
            if(productTranslate != null)
            {
                productGetDto.Name = productTranslate.Name ?? productGetDto.Name;
                productGetDto.Slug = productTranslate.Slug ?? productGetDto.Slug;
                productGetDto.ShortDescription = productTranslate.ShortDescription ?? productGetDto.ShortDescription;
                productGetDto.Description = productTranslate.Description ?? productGetDto.Description;
                productGetDto.QuantityPerPackage = productTranslate.QuantityPerPackage ?? productGetDto.QuantityPerPackage;
                productGetDto.Volume = productTranslate.Volume ?? productGetDto.Volume;
                productGetDto.PackageType = productTranslate.PackageType ?? productGetDto.PackageType;
                productGetDto.Ingredients = productTranslate.Ingredients ?? productGetDto.Ingredients;
                productGetDto.Benefits = productTranslate.Benefits ?? productGetDto.Benefits;
                productGetDto.UsageInstructions = productTranslate.UsageInstructions ?? productGetDto.UsageInstructions;
                productGetDto.Warnings = productTranslate.Warnings ?? productGetDto.Warnings;
                productGetDto.Brand = productTranslate.Brand ?? productGetDto.Brand;
                productGetDto.CountryOfOrigin = productTranslate.CountryOfOrigin ?? productGetDto.CountryOfOrigin;
                productGetDto.ShelfLife = productTranslate.ShelfLife ?? productGetDto.ShelfLife;
            }
            return productGetDto;
        }
        public async Task CreateAsync(ProductCreateDto productDto)
        {
            var result = await _productRepo.IsExistAsync(x => x.Name == productDto.Name);
            if (result)
                throw new AlreadyExistException("This Name alredy exist");
            if (!await _categoryRepo.IsExistAsync(x => x.Id == productDto.CategoryId || productDto.CategoryId != 0))
                throw new NotFoundException("You don't have this category");
            Product product = _mapper.Map<Product>(productDto);
            product.ProductTags = new List<ProductTag>();
            foreach (var tagId in productDto.TagIds)
            {
                if (!await _tagRepo.IsExistAsync(x => x.Id == tagId)) throw new NotFoundException("You don't have this tag");
                product.ProductTags.Add(new ProductTag { TagId = tagId });
            }
            await _productRepo.AddAsync(product);
            await _productRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _productRepo.GetByIdAsync(id);
            if (product == null) throw new NotFoundException("Not Found");
            await _productRepo.DeleteAsync(product);
        }
        public async Task UpdateAsync(ProductUpdateDto productDto, int id)
        {
            Product existed = await _productRepo.GetByIdAsync(id, true, includes: new string[] {nameof(Product.ProductTags) });
            if (existed.Name != productDto.Name)
                if (await _productRepo.IsExistAsync(x => x.Name == productDto.Name)) throw new Exception("You have this name product please send other name");
            if (productDto.CategoryId != existed.CategoryId)
            {
                if (!await _categoryRepo.IsExistAsync(x => x.Id == productDto.CategoryId)) throw new Exception("You don't have this category in your categoryies");
            }
            existed = _mapper.Map(productDto, existed);
            foreach (var tagid in productDto.TagIds)
            {
                if (!existed.ProductTags.Any(pc => pc.TagId == tagid))
                {
                    if (!await _tagRepo.IsExistAsync(x => x.Id == tagid)) throw new Exception("You don't have this tag");
                    existed.ProductTags.Add(new ProductTag { TagId = tagid, });
                }
            }

            existed.ProductTags = existed.ProductTags.Where(x => productDto.TagIds.Any(tagid => x.TagId == tagid)).ToList();

            await _productRepo.UpdateAsync(existed);
            await _productRepo.SaveChangesAsync();
        }
    }
}
