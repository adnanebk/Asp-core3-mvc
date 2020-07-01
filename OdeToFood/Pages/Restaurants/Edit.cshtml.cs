using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IData<Restaurant> _data;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IData<Restaurant> data,
                         IHtmlHelper htmlHelper)
        {
            this._data = data;
            this.htmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = _data.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {               
            if(!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();                
            }

            if (Restaurant.Id > 0)
            {
                _data.Update(Restaurant);
            }
            else
            {
                _data.Add(Restaurant);
            }
            _data.Commit();
            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}