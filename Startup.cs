using System;
using ASP_Core_Hemtenta.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASP_Core_Hemtenta
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthorization(options =>
                options.AddPolicy("ShouldBeAdministrator",
                    policy => policy.RequireRole("Administrator")));
            services.AddRazorPages(options =>
                options.Conventions.AuthorizeAreaFolder("Admin", "/", "ShouldBeAdministrator"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
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
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "productBySlug",
                    pattern: "product/{slug}",
                    defaults: new { controller = "Product", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "categoryBySlug",
                    pattern: "category/{slug}",
                    defaults: new { controller = "Category", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });

            MyIdentityDataInitializer.SeedData(userManager, roleManager);
        }

        public static class MyIdentityDataInitializer
        {
            public static void SeedData(
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager)
            {
                SeedRoles(roleManager);
                SeedUsers(userManager);
            }

            public static void SeedUsers(UserManager<IdentityUser> userManager)
            {
                if (userManager.FindByNameAsync("john.doe@gmail.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "john.doe@gmail.com",
                        Email = "john.doe@gmail.com",
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Secret#123").Result;
                }


                if (userManager.FindByNameAsync("jane.doe@nomail.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "jane.doe@nomail.com",
                        Email = "jane.doe@nomail.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Secret#123").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Administrator").Wait();
                    }
                }
            }

            public static void SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                if (!roleManager.RoleExistsAsync("Administrator").Result)
                {
                    IdentityRole role = new IdentityRole
                    {
                        Name = "Administrator"
                    };

                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }


        }
    }
}
