using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IData<Restaurant> _data;
         
                 [TempData]
                 public string Message { get; set; }
         
                 public Restaurant Restaurant { get; set; }
         
                 public DetailModel(IData<Restaurant> data)
                 {
                     this._data = data;
                 }
         
                 public IActionResult OnGet(int restaurantId)
                 {
                     Restaurant = _data.GetById(restaurantId);
                     if(Restaurant == null)
                     {
                         return RedirectToPage("./NotFound");
                     }
                     return Page();
                 }
    }
}