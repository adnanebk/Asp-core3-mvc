using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood
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

            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlite("Data Source=MyDb.db");
                
            });
       
            services.AddIdentity<OwnUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<OdeToFoodDbContext>();
            services.AddScoped<IData<Restaurant>, DataRestaurant>();
            services.AddScoped<IData<MenuLine>, DataMenu>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // aspnetcore30
            services.AddRazorPages();
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            app.Use(SayHelloMiddleware);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNodeModules();

            // aspnetcore30
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(e =>
            {
                e.MapDefaultControllerRoute();
                e.MapRazorPages();
               // e.MapControllers();
           
            });
        }

        private RequestDelegate SayHelloMiddleware(
                                    RequestDelegate next)
        {
            return async ctx =>
            {

                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello, World!");
                }
                else
                {
                    await next(ctx);                    
                }
            };
        }
    }
}
