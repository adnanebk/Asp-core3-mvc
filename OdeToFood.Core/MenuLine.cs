using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
    public class MenuLine
    {
        public int Id { get; set; }
       [Required]
       [Display(Name = "product name")]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,10000)]
        public double  Price{ get; set; }
        public string Photo { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}