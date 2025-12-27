using AutoMapper;
using FermentaLabOnion.Application.DTOs.ReviewDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewGetDto>()
                .ForMember(d => d.UserName,
                    o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.UserImage,
                    o => o.MapFrom(s => s.AppUser.ProfileImage));
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewApproveDto, Review>().ReverseMap();
        }
    }
}
