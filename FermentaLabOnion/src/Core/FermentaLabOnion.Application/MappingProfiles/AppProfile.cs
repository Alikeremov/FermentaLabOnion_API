using AutoMapper;
using FermentaLabOnion.Application.DTOs.AppDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<App, AppGetDto>();
            CreateMap<AppCreateDto, App>();
            CreateMap<App, AppUpdateDto>().ReverseMap();
        }
    }
}
