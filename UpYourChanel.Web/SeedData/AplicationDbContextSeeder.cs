using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.SeedData
{
    public class AplicationDbContextSeeder
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AplicationDbContextSeeder(IServiceProvider serviceProvider, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = serviceProvider.GetService<UserManager<User>>();
            this.roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
        }
        public async Task SeedDataAsync()
        {
            await SeedUsersAsync();
            await SeedRolesAsync();
            await SeedUsersAndRolesAsync();
        }

        private async Task SeedUsersAndRolesAsync()
        {
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);
            var user = await userManager.FindByNameAsync(GlobalConstants.AdminName);

            var exists = dbContext.UserRoles.Any(x => x.RoleId == role.Id || x.UserId == user.Id);
            if (exists)
            {
                return;
            }

            await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>()
            {
                RoleId = role.Id,
                UserId = user.Id
            });
            await dbContext.SaveChangesAsync();
        }

        private async Task SeedRolesAsync()
        {
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);

            if (role != null)
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = GlobalConstants.AdminRoleName,
            });
        }

        private async Task SeedUsersAsync()
        {
            var user = await userManager.FindByNameAsync(GlobalConstants.AdminName);

            if (user != null)
            {
                return;
            }

            await userManager.CreateAsync(new User()
            {
                UserName = GlobalConstants.AdminName,
                Email = GlobalConstants.AdminEmail,
                EmailConfirmed = true,
            },
            password: GlobalConstants.AdminPassword);
        }
    }
}
