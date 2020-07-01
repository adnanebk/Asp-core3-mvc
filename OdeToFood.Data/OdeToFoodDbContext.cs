using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : IdentityDbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<OwnUser> OwnUsers { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<MenuLine> MenuLines { get; set; }


        public void seed()
        {
            MenuLine m1=new MenuLine(){ProductName = "Pizza",Price = 6,Description = "Pizza margarita",Photo = "http://www.greenitalia.ie/images/welcome_img1.jpg"};
            MenuLine m2=new MenuLine(){ProductName = "Tajine",Price = 5,Description = "Moroccan tajine",Photo = "https://du7ybees82p4m.cloudfront.net/617_536262ac30faa.jpg"};
            MenuLine m3=new MenuLine(){ProductName = "Sushi",Price = 7,Description = "Japanese sushi",Photo = "https://images.japancentre.com/recipes/pics/18/main/makisushi.jpg"};
             OwnUser ownUser=new OwnUser(){Email = "aa@aa.aa",UserName = "aa@aa.aa",PasswordHash = new PasswordHasher<OwnUser>().HashPassword(null,"aaaa")};
            var admin= OwnUsers.Add(ownUser).Entity; 
            SaveChanges();
            var resto = new Restaurant("Restaurant 1", "Morocco", 0, CuisineType.Moroccan, m2, "this is a moroccan restaurant",new Rate(){RestoRating = 2,OwnUser = admin});

            List<Restaurant> restaurants=new List<Restaurant>()
            {
                resto,
                new Restaurant("Restaurant 2","Italy",0,CuisineType.Italian,m1,"this is a Italian restaurant",new Rate(){RestoRating = 2,OwnUser = admin}),
                new Restaurant("Restaurant 3","China",0,CuisineType.Chinese,null,"this is a Chinese restaurant",new Rate(){RestoRating = 2,OwnUser = admin}),
                new Restaurant("Restaurant 4","Japan",0,CuisineType.Japanese,m3,"this is a Japanese restaurant",new Rate(){RestoRating = 2,OwnUser = admin}),
            };
            AddRange(restaurants);
            SaveChanges();
                // OwnUser ownUser=new OwnUser(){Email = "aa@aa.aa",PasswordHash = new PasswordHasher<OwnUser>().HashPassword(null,"aaaa")};
      // var admin= OwnUsers.Add(ownUser).Entity;
       // SaveChanges();
        //Rate rate=new Rate(){RestoRating = 2,OwnUser = admin};
       //var aRate= Rates.Add(new Rate(){RestoRating = 2,OwnUser = null}).Entity;
       // SaveChanges();
            /*restaurants.ForEach(r =>
            {
                r.Rates=new List<Rate>();
                r.Rates.Add(new Rate(){RestoRating = 2,OwnUser = null});
                Restaurants.Update(r);
                SaveChanges();
            });*/
        }
    }
}
