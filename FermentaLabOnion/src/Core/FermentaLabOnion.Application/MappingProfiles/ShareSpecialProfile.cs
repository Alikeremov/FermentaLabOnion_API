using AutoMapper;
using FermentaLabOnion.Application.DTOs.ShareSpecialDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ShareSpecialProfile : Profile
    {
        public ShareSpecialProfile()
        {
            CreateMap<ShareSpecial, ShareSpecialGetDto>();
            CreateMap<ShareSpecialCreateDto, ShareSpecial>();
            CreateMap<ShareSpecial, ShareSpecialUpdateDto>().ReverseMap();
        }
    }
}
