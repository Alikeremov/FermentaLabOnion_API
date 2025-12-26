using AutoMapper;
using FermentaLabOnion.Application.DTOs.ChineseWisdomDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ChineseWisdomProfile : Profile
    {
        public ChineseWisdomProfile()
        {
            CreateMap<ChineseWisdom, ChineseWisdomGetDto>();
            CreateMap<ChineseWisdomCreateDto, ChineseWisdom>();
            CreateMap<ChineseWisdom, ChineseWisdomUpdateDto>().ReverseMap();
        }
    }
}
