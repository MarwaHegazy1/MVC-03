using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using ASP.NET_Core_MVC.DAL.Data;
using ASP.NET_Core_MVC_03.PL.Extensions;
using ASP.NET_Core_MVC_03.PL.Helpers;
using ASP.NET_Core_MVC_03.PL.Services.EmailSender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC_03.PL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            ///services.AddScoped<ApplicationDbContext>();
            ///services.AddScoped<DbContextOptions<ApplicationDbContext>>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }).AddApplicationServices();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            ///services.AddScoped<UserManager<ApplicationUser>>();
            ///services.AddScoped<SignInManager<ApplicationUser>>();
            ///services.AddScoped<RoleManager<IdentityRole>>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

			/// services.AddAuthentication();
			///services.ConfigureApplicationCookie(options =>
			///{
			///	//options.LogoutPath = "";
			///	options.LoginPath = "/Account/SignIn";
			///	options.ExpireTimeSpan = TimeSpan.FromDays(1);
			///	options.AccessDeniedPath = "/Home/Error";
			///});
            ///services.AddAuthentication("Hamda");
            ///services.AddAuthentication(options =>
            ///{
            ///    options.DefaultAuthenticateScheme = "Hamda";
            ///})
            ///    .AddCookie("Hamda", options =>
            ///    {
            ///        options.LoginPath = "/Account/SignIn";
            ///        options.ExpireTimeSpan = TimeSpan.FromDays(1);
            ///        options.AccessDeniedPath = "/Home/Error";
            ///    });
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
  