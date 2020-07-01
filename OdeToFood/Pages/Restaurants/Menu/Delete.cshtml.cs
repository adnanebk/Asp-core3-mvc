using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants.Menu
{
    public class DeleteModel : PageModel
    {
        private readonly IData<MenuLine> _data;

        public MenuLine MenuLine { get; set; }
        [BindProperty]
        public int RestaurantId { get; set; }

        public DeleteModel(IData<MenuLine> data)
        {
            this._data = data;
        }

        public IActionResult OnGet(int menuLineId)
        {
            MenuLine = _data.GetById(menuLineId);
            if(MenuLine == null)
            {
                return RedirectToPage("./NotFound");
            }

            RestaurantId = MenuLine.RestaurantId;
            return Page();
        }

        public IActionResult OnPost(int menuLineId)
        {
            var menuLine = _data.Delete(menuLineId);
            _data.Commit();

            if(menuLine == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"{menuLine.ProductName} deleted";
            return RedirectToPage("/Restaurants/Detail",new{RestaurantId});
        }
    }
}