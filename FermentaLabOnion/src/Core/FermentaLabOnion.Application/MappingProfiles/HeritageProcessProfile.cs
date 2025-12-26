using AutoMapper;
using FermentaLabOnion.Application.DTOs.HeritageProcessDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class HeritageProcessProfile : Profile
    {
        public HeritageProcessProfile()
        {
            CreateMap<HeritageProcess, HeritageProcessGetDto>();
            CreateMap<HeritageProcessCreateDto, HeritageProcess>();
            CreateMap<HeritageProcess, HeritageProcessUpdateDto>().ReverseMap();
        }
    }
}
