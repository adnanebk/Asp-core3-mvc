using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly OdeToFoodDbContext _context;

        public RestaurantsController(OdeToFoodDbContext context)
        {
            _context = context;
   
        }

        // GET: api/Restaurants
        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants()
        {
            return _context.Restaurants;
        }

        [HttpPost("{id}/rating")]
        public async void UpdateRating([FromRoute] int id,[FromBody] int rating)
        {
           OwnUser ownUser= await _context.OwnUsers.FirstOrDefaultAsync(u=>u.Email.Equals(HttpContext.User.Identity.Name));
           if (ownUser != null && rating>=0)
           {
               var restaurant = await _context.Restaurants.Where(r=>r.Id==id).Include(r=>r.Rates).ThenInclude(u=>u.OwnUser).FirstAsync();
               //var restaurant =  _context.Restaurants. Include(r=>r.Rates.Where(r=>r.OwnUser.Email.Equals(HttpContext.User.Identity.Name))).FirstOrDefaultAsync(r=>r.Id==id).Result;
               
               var rate = restaurant.Rates.FirstOrDefault(r => r.OwnUser.Email.Equals(HttpContext.User.Identity.Name));
               if (rate!=null)
               {
                   rate.RestoRating = rating;
                   _context.Rates.Update(rate);
                   await  _context.SaveChangesAsync();
               }
               else
               {
                   var newRate = new Rate() {RestoRating = rating, OwnUser = ownUser};
                   Rate aRate=  _context.Rates.AddAsync(newRate).Result.Entity;
                   await  _context.SaveChangesAsync();
                        restaurant.Rates.Add(aRate);
                              Restaurant resto= _context.Restaurants.Update(restaurant).Entity;
                           await  _context.SaveChangesAsync();    
               }
              
          
           }

        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // PUT: api/Restaurants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant([FromRoute] int id, [FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Restaurants
        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return Ok(restaurant);
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}