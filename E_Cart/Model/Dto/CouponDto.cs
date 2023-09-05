using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Cart.Model.Dto
{
    public class CouponDto
    {
        public Guid CouponId { get; set; }
       
        public string CouponCode { get; set; } = string.Empty;
 
        public int CouponAmount { get; set; }
    
        public int CouponMinAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}