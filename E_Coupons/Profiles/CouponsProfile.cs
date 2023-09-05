using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Coupons.Model;
using E_Coupons.Model.Dtos;

namespace E_Coupons.Profiles
{
    public class CouponsProfile:Profile
    {

        public CouponsProfile()
        {
            CreateMap<CouponRequestDto,Coupon>().ReverseMap();
        }
    }
}