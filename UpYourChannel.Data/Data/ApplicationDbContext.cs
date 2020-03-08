using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Video> Videos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          //  options.UseSqlServer(connection, b => b.MigrationsAssembly("UpYourChannel.Web"));
        }
    }
}
