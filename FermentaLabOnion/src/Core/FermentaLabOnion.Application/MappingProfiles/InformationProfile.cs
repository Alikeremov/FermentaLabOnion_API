using AutoMapper;
using FermentaLabOnion.Application.DTOs.InformationDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class InformationProfile : Profile
    {
        public InformationProfile()
        {
            CreateMap<Information, InformationGetDto>();
            CreateMap<InformationCreateDto, Information>();
            CreateMap<Information, InformationUpdateDto>().ReverseMap();
        }
    }
}
