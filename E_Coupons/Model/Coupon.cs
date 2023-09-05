using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
﻿using System.ComponentModel.DataAnnotations;

namespace E_Coupons.Model
{
    public class Coupon
    {
        [Key]
        public Guid CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }= string.Empty;
        [Required]
        public int CouponAmount { get; set; }
        [Required]
        public int CouponMinAmont { get; set; }

        public DateTime CreatedAt { get; set; }=DateTime.Now;
    }
}