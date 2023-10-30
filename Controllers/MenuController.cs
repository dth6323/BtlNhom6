using Azure;
using BtlNhom6.Data;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BtlNhom6.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private AuthDbContext db;

        public MenuController(AuthDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(string sortOrder, int? page, string searchString)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            int pagesize = 6;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            IQueryable<Menu> product; 
            switch (sortOrder)
            {
                case "price_desc":
                    product = db.Menus.AsNoTracking().OrderByDescending(x => x.totalprice);
                    break;
                case "Price":
                    product = db.Menus.AsNoTracking().OrderBy(x => x.totalprice);
                    break;
                case "date_desc":
                    product = db.Menus.AsNoTracking().OrderByDescending(x => x.DateTime);
                    break;
                case "Date":
                    product = db.Menus.AsNoTracking().OrderBy(x => x.DateTime);
                    break;
                default:
                    product = db.Menus.AsNoTracking();
                    break;
            }
            product = product.Include(m => m.User).AsNoTracking();
            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.User.Address.Contains(searchString));
            }
            PagedList<Menu> lst = new PagedList<Menu>(product, pagenumber, pagesize);
            return View(lst);
        }            
        public IActionResult Delete(int id)
        {
            if (db.Menus == null)
                return Problem("Enity set 'menu' is null");
            var menu = db.Menus.Find(id);
            if (menu != null)
                db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {

            var item = db.MenuDetails.Where(x => x.MenuID == id).ToList();
            return View(item);
        }
    }
}
