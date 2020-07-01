using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants.Menu
{
    public class EditModel : PageModel
    {
  
        private readonly IData<MenuLine> _data;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public MenuLine Menu { get; set; }

        public EditModel(IData<MenuLine> data,IWebHostEnvironment hostEnvironment)
        {
            this._data = data;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet(int? menuId,int restaurantId)
        {
           
            if (menuId.HasValue)
            {
                Menu = _data.GetById(menuId.Value);
                Menu.RestaurantId = restaurantId;
            }
            else
            {
                Menu = new MenuLine();
                Menu.RestaurantId = restaurantId;
            }
            if(Menu == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(IFormFile photo)
        {               
            if(!ModelState.IsValid)
            {
                return Page();                
            }

            string fileName = "";
            if (photo != null)
            {
                string uploadedFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadedFolder, fileName);
                photo.CopyTo(new FileStream(filePath,FileMode.Create));
                Menu.Photo = "/images/"+fileName;
            }
        
            if (Menu.Id > 0)
            {
                _data.Update(Menu);
            }
            else
            {
                _data.Add(Menu);
            }
            _data.Commit();
            TempData["Message"] = "Menu saved!";
            return RedirectToPage("/restaurants/Detail", new { restaurantId = Menu.RestaurantId});
        }
    }
}