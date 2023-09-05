using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model;
using E_Cart.Model.Dto;
﻿using AutoMapper;

namespace E_Cart.Profiles
{
    public class CartProfiles: Profile
    {
        public CartProfiles()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}