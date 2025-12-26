using AutoMapper;
using FermentaLabOnion.Application.DTOs.InformationTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class InformationTranslateProfile : Profile
    {
        public InformationTranslateProfile()
        {
            CreateMap<InformationTranslate, InformationTranslateGetDto>();
            CreateMap<InformationTranslateCreateDto, InformationTranslate>();
            CreateMap<InformationTranslate, InformationTranslateUpdateDto>().ReverseMap();
        }
    }
}
