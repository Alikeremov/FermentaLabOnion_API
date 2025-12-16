using AutoMapper;
using FermentaLabOnion.Application.DTOs.ProductTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ProductTranslateProfile: Profile
    {
        public ProductTranslateProfile()
        {
            CreateMap<ProductTranslate, ProductTranslateGetDto>();
            CreateMap<ProductTranslateCreateDto, ProductTranslate>();
            CreateMap<ProductTranslate, ProductTranslateUpdateDto>().ReverseMap();
        }
    }
}
