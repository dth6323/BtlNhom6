﻿using BtlNhom6.Areas.Identity.Data;
using BtlNhom6.Data;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using X.PagedList;

namespace BtlNhom6.Controllers
{
    [DefaultBreadcrumb("ViewData.Title")]
    public class ClientController : Controller
    {
        private AuthDbContext db;
        public ClientController(AuthDbContext db)
        {
			this.db = db;
        }

		public IActionResult Index(int? page, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            int pagesize = 6;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            IQueryable<Dish> product;
            product = db.dishes.AsNoTracking().OrderBy(x=>x.DishName);
            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.DishName.Contains(searchString));
            }
            PagedList<Dish> lst = new PagedList<Dish>(product,pagenumber,pagesize);
            if (HttpContext.Session.Get<List<Dish>>("Cart") == null)
            {
                ViewBag.cartNumber = 0;
            }
            else
            {
                ViewBag.cartNumber = HttpContext.Session.Get<List<Dish>>("Cart").Count;
            }
            return View(lst);
        }
        [Breadcrumb(FromAction = "Index", Title = "Details")]
        public IActionResult Details(int id)
        {
            var item = db.dishes.FirstOrDefault(x=>x.DishID == id);
            if (HttpContext.Session.Get<List<Dish>>("Cart") == null)
            {
                ViewBag.cartNumber = 0;
            }
            else
            {
                ViewBag.cartNumber = HttpContext.Session.Get<List<Dish>>("Cart").Count;
            }
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}
