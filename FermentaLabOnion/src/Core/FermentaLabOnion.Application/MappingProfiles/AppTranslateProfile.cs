using AutoMapper;
using FermentaLabOnion.Application.DTOs.AppTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class AppTranslateProfile : Profile
    {
        public AppTranslateProfile()
        {
            CreateMap<AppTranslate, AppTranslateGetDto>();
            CreateMap<AppTranslateCreateDto, AppTranslate>();
            CreateMap<AppTranslate, AppTranslateUpdateDto>().ReverseMap();
        }
    }
}
