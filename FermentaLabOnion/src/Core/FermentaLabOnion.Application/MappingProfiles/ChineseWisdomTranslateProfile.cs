using AutoMapper;
using FermentaLabOnion.Application.DTOs.ChineseWisdomDTOs;
using FermentaLabOnion.Application.DTOs.ChineseWisdomTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ChineseWisdomTranslateProfile : Profile
    {
        public ChineseWisdomTranslateProfile()
        {
            CreateMap<ChineseWisdomTranslate, ChineseWisdomTranslateGetDto>();
            CreateMap<ChineseWisdomTranslateCreateDto, ChineseWisdomTranslate>();
            CreateMap<ChineseWisdomTranslate, ChineseWisdomTranslateUpdateDto>().ReverseMap();
        }
    }
}
