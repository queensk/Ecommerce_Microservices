using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Coupons.Model;

namespace E_Coupons.Services.IService
{
    public interface ICouponInterface
    {
        Task<IEnumerable<Coupon>> GetCouponsAsync();

        Task<Coupon> GetCouponByIdAsync(Guid id);

        Task<Coupon> GetCouponByNameAsync(string couponCode);

        Task<string> AddCouponAsync(Coupon coupon);
        Task<string> UpdateCouponAsync(Coupon coupon);
        Task<string> DeleteCouponAsync(Coupon coupon);
    }
}