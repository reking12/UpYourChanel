using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UpYourChannel.Data.Data;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.SeedData;
using UpYourChannel.Data.Models;
using AutoMapper;
using UpYourChannel.Web.MappingConfiguration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;

namespace UpYourChannel.Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequiredUniqueChars = 0;
                 options.User.RequireUniqueEmail = true;
             }
           ).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
            services.AddRazorPages();
            // add options for https
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
            services.AddAutoMapper(c =>
                c.AddProfile<UpYourChannelProfile>(), typeof(Startup));
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/Account/Login");

            services.AddTransient<ICloudinaryService>(
            serviceProvider => new CloudinaryService(
            Configuration.GetConnectionString("CLOUD_NAME"),
            Configuration.GetConnectionString("API_KEY"),
            Configuration.GetConnectionString("API_SECRET")));

            services.AddTransient<IVideoService, VideoService>();
            services.AddTransient<IRequestedVideoService, RequestedVideoService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IMessageService, MessageService>();

            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new AplicationDbContextSeeder(serviceScope.ServiceProvider, dbContext).SeedDataAsync().GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
                     name: "areaRoute",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
