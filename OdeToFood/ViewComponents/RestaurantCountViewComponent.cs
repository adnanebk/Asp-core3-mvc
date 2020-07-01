using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Core;

namespace OdeToFood.ViewComponents
{
    public class RestaurantCountViewComponent
         : ViewComponent
    {
        private readonly IData<Restaurant> _data;

        public RestaurantCountViewComponent(IData<Restaurant> data)
        {
            this._data = data;
        }

        public IViewComponentResult Invoke()
        {
            var count = _data.GetCount();
            return View(count);
        }

    }
}
