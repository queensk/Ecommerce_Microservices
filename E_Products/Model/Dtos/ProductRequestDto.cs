using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Products.Model.Dtos
{
    public class ProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}