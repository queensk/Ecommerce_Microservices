using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Cart.Model
{
    public class CartHeader
    {
        [Key]
        public Guid CartHeaderId { get; set; }

        [Required]
        public Guid UserId { get; set; }


        public string? CouponCode { get; set; } = string.Empty;

        //do not create a column for this --- this is not going to the database 
        [NotMapped]
        public int Discount { get; set; }

        [NotMapped]
        public int CartTotal { get; set; }
    }
}