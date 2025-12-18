using AutoMapper;
using FermentaLabOnion.Application.DTOs.CategoryTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class CategoryTranslateProfile:Profile
    {
        public CategoryTranslateProfile()
        {
            CreateMap<CategoryTranslate, CategoryTranslateGetDto>();
            CreateMap<CategoryTranslateCreateDto, CategoryTranslate>();
            CreateMap<CategoryTranslate, CategoryTranslateUpdateDto>().ReverseMap();
        }
    }
}
