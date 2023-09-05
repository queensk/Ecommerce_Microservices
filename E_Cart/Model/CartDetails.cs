using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model.Dto;

namespace E_Cart.Model
{
    public class CartDetails
    {
        [Key]
        public Guid CartDetailsId { get; set; }


        public Guid CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }


        public Guid ProductId { get; set; }

        [NotMapped]
        public ProductDto? Product { get; set; }

        public int Count { get; set; }
    }
}