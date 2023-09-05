using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Products.Model.Dtos
{
    public class ProductDto
    {
        Guid Id { get; set; }
        string Name { get; set; } = string.Empty;
        string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public String Category { get; set; } = string.Empty;
        public String ImageUrl { get; set; } = string.Empty;
    }
}   
