using AutoMapper;
using FermentaLabOnion.Application.DTOs.AutenticationDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<LoginDto, AppUser>().ReverseMap();
            CreateMap<AppUserGetDto, AppUser>().ReverseMap();
        }
    }
}
