using AutoMapper;
using FermentaLabOnion.Application.DTOs.TagDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class TagProfile: Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagGetDto>();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<Tag, TagUpdateDto>().ReverseMap();
        }
    }
}
