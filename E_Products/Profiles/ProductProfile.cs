using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Products.Model;
using E_Products.Model.Dtos;

namespace E_Products.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestDto, Product>().ReverseMap();
        }
    }
}