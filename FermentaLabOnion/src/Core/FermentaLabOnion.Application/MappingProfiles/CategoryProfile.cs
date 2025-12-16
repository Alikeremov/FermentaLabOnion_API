using AutoMapper;
using FermentaLabOnion.Application.DTOs.CategoryDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        }
    }
}
