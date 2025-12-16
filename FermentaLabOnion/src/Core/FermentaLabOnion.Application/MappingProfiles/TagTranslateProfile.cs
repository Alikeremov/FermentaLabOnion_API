using AutoMapper;
using FermentaLabOnion.Application.DTOs.TagTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class TagTranslateProfile: Profile
    {
        public TagTranslateProfile()
        {
            CreateMap<TagTranslate, TagTranslateGetDto>();
            CreateMap<TagTranslateCreateDto, TagTranslate>();
            CreateMap<TagTranslate, TagTranslateUpdateDto>().ReverseMap();
        }
    }
}
