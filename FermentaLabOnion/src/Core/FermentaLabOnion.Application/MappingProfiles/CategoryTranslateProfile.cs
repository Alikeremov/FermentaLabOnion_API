using AutoMapper;
using FermentaLabOnion.Application.DTOs.CategoryTranslateDTOs;
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
            CreateMap<CategoryTranslateProfile, CategoryTranslateGetDto>();
            CreateMap<CategoryTranslateCreateDto, CategoryTranslateProfile>();
            CreateMap<CategoryTranslateProfile, CategoryTranslateUpdateDto>().ReverseMap();
        }
    }
}
