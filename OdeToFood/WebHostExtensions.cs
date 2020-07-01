using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OdeToFood.Data;

namespace OdeToFood
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<OdeToFoodDbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureDeleted();
                        appContext.Database.EnsureCreated();
                        appContext.seed();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }

            return webHost;
        }
    }
}