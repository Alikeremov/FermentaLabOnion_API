using AutoMapper;
using FermentaLabOnion.Application.DTOs.QuestionTranslateDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class QuestionTranslateProfile : Profile
    {
        public QuestionTranslateProfile()
        {
            CreateMap<QuestionTranslate, QuestionTranslateGetDto>();
            CreateMap<QuestionTranslateCreateDto, QuestionTranslate>();
            CreateMap<QuestionTranslate, QuestionTranslateUpdateDto>().ReverseMap();
        }
    }
}
