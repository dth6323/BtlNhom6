﻿using Azure;
using BtlNhom6.Data;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BtlNhom6.Controllers
{
    public class MenuController : Controller
    {
        private AuthDbContext db;

        public MenuController(AuthDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int? page)
        {
            int pagesize = 6;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var product = db.Menus.AsNoTracking().OrderBy(x => x.totalprice);
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
