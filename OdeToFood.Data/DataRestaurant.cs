using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class DataRestaurant : IData<Restaurant>
    {
        
        private readonly OdeToFoodDbContext db;

        public DataRestaurant(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Include(r=>r.MenuLines).FirstOrDefault(r=>r.Id==id);
        }

        public int GetCount()
        {
            return db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetByName(string name)
        {
            var query = from r in db.Restaurants.Include(r=>r.Rates)
                where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                orderby r.Name
                select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
        /*public User AddUser(string id)
        {
            User user = null;
            if (db.Users.Any(u => u.Id == id))
                user = db.Users.Find(id);
            else
                user = db.Users.Add(new User() {Id = id}).Entity;
            return user;

        }*/
       
    }
}