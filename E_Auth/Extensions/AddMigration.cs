using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auth.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Auth.Extensions
{
    public static class AddMigration
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }
            return app;
        }
    }
}