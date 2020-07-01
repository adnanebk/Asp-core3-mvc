using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OdeToFood.Core
{
    public class Restaurant 
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Location { get; set; }
        
        public string Description { get; set; }

        public int Rating { get; set; } 
        public List<Rate> Rates { get; set; }=new List<Rate>();
    
        public CuisineType Cuisine { get; set; }

        public List<MenuLine> MenuLines { get; set; }
        public Restaurant()
        {
        }

        public Restaurant(string name, string location, int rating, CuisineType cuisine,MenuLine menuLine,string description,Rate rate)
        {
            Description = description;
            Name = name;
            Location = location;
            Rating = rating;
            Cuisine = cuisine;
            MenuLines=new List<MenuLine>();
            if(menuLine!=null)
            MenuLines.Add(menuLine);
            Rates.Add(rate);
        }
        public Restaurant(string name, string location, int rating, CuisineType cuisine)
        {
            Name = name;
            Location = location;
            Rating = rating;
            Cuisine = cuisine;
        }
    }

}
