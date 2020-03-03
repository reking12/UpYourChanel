using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UpYourChanel.Web.Models;

namespace UpYourChanel.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Video> Videos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
