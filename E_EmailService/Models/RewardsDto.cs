using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_EmailService.Models
{
    public class RewardsDto
    {
        public string? UserId { get; set; }

        public string? Email { get; set; }

        public int TotalAmount { get; set; }    
    }
}