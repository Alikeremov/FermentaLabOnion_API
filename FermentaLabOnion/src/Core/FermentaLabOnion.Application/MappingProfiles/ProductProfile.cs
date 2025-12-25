using AutoMapper;
using FermentaLabOnion.Application.DTOs.ProductDTOs;
using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDto>()
                .ForMember(d => d.Tags,o => o.MapFrom(s =>s.ProductTags.Select(pt => pt.Tag.Name)))
                .ForMember(d => d.TagIds,o => o.MapFrom(s => s.ProductTags.Select(pt => pt.TagId)))
                .ForMember(d => d.Images, o => o.MapFrom(s => s.ProductImages)); ;
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }

}
