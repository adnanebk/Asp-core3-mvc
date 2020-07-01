using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IData<Restaurant> _data;
        private readonly ILogger<ListModel> logger;
        private readonly IHttpContextAccessor _accessor;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, 
                         IData<Restaurant> data,
                         ILogger<ListModel> logger,IHttpContextAccessor accessor)
        {
            this.config = config;
            this._data = data;
            this.logger = logger;
            _accessor = accessor;


            //var adress= _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
           // _data.AddUser(adress);
        }

        public IActionResult OnGet()
        {
            logger.LogError("Executing ListModel");
            Message = config["Message"];
            Restaurants = _data.GetByName(SearchTerm);
            //if (!_dataUser.AnyUser())
                // RedirectToAction("RegisterAdmin", "Account");
            return Page();
        }
    }
}