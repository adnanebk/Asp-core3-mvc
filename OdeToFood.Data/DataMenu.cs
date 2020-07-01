using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class DataMenu:IData<MenuLine>
    {
        private readonly OdeToFoodDbContext db;

        public DataMenu(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public MenuLine Add(MenuLine newMenuLine)
        {
            db.Add(newMenuLine);
            return newMenuLine;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

    

        public MenuLine Delete(int id)
        {
            var MenuLine = GetById(id);
            if(MenuLine != null)
            {
                db.MenuLines.Remove(MenuLine);
            }
            return MenuLine;
        }

        public IEnumerable<MenuLine> GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public MenuLine GetById(int id)
        {
            return db.MenuLines.Find(id);
        }

        public int GetCount()
        {
            return db.MenuLines.Count();
        }

        public MenuLine Update(MenuLine updatedMenuLine)
        {
        //  Restaurant r=  db.Restaurants.First(r => r.MenuLines.Any(m => m.Id == updatedMenuLine.Id));
        
                    var entity = db.MenuLines.Attach(updatedMenuLine);
                   // var r = db.MenuLines.Include(m=>m.Restaurant).AsQueryable().FirstOrDefault(m=>m.Id==updatedMenuLine.Id).Restaurant;
        //updatedMenuLine.RestaurantId = r.Id;
        entity.State = EntityState.Modified;

            return updatedMenuLine;
        }
    }
}