using AutoMapper;
using Demo1.Application.DTO;
using Demo1.Application.DTOs;
using Demo1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //map product
            CreateMap<Product, ProductDto>().ReverseMap();
            //map category
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
