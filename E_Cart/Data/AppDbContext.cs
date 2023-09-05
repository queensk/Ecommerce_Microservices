using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Cart.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<CartDetails> CartDetails { get; set; }

        public DbSet<CartHeader> CartHeaders { get; set; }
    }
}