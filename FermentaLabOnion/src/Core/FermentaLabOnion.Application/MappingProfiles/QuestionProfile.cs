using AutoMapper;
using FermentaLabOnion.Application.DTOs.QuestionDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionGetDto>();
            CreateMap<QuestionCreateDto, Question>();
            CreateMap<Question, QuestionUpdateDto>().ReverseMap();
        }
    }
}
