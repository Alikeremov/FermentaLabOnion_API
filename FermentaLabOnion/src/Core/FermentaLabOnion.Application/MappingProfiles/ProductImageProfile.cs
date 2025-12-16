using AutoMapper;
using FermentaLabOnion.Application.DTOs.ProductImageDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ProductImageProfile: Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageGetDto>();
            CreateMap<ProductImageCreateDto, ProductImage>();
            CreateMap<ProductImage, ProductImageUpdateDto>().ReverseMap();
        }
    }
}
