using BtlNhom6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace BtlNhom6.ViewComponents
{
    public class RenderViewComponent:ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>()
			{
				new MenuItem(){Id=1,Name="Dish",Link="Dish/index"},
				new MenuItem(){Id=2,Name="Menu",Link="Menu/index"},
				new MenuItem(){Id=3,Name="Employees",Link="Employee/index"}
			};

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}
