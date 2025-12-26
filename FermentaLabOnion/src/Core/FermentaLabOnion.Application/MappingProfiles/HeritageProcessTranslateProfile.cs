using AutoMapper;
using FermentaLabOnion.Application.DTOs.HeritageProcessTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class HeritageProcessTranslateProfile : Profile
    {
        public HeritageProcessTranslateProfile()
        {
            CreateMap<HeritageProcessTranslate, HeritageProcessTranslateGetDto>();
            CreateMap<HeritageProcessTranslateCreateDto, HeritageProcessTranslate>();
            CreateMap<HeritageProcessTranslate, HeritageProcessTranslateUpdateDto>().ReverseMap();
        }
    }
}
