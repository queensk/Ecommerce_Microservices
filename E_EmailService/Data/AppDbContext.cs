using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_EmailService.Models;
using Microsoft.EntityFrameworkCore;

namespace E_EmailService.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
        public DbSet<EmailLoggers> EmailLoggers { get; set; }
    }
}