using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Coupons.Model.Dtos
{
    public class CouponRequestDto
    {
        public string CouponCode { get; set; } = string.Empty;

        public int CouponAmount { get; set; }

        public int CouponMinAmount { get; set; }
    }
}