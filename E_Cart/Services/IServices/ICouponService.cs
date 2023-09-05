using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model.Dto;

namespace E_Cart.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponData(string CouponCode);
    }
}