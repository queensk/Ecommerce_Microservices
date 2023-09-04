using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Auth.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}