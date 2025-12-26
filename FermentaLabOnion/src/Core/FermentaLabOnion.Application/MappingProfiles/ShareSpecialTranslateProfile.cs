using AutoMapper;
using FermentaLabOnion.Application.DTOs.ShareSpecialTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ShareSpecialTranslateProfile : Profile
    {
        public ShareSpecialTranslateProfile()
        {
            CreateMap<ShareSpecialTranslate, ShareSpecialTranslateGetDto>();
            CreateMap<ShareSpecialTranslateCreateDto, ShareSpecialTranslate>();
            CreateMap<ShareSpecialTranslate, ShareSpecialTranslateUpdateDto>().ReverseMap();
        }
    }
}
