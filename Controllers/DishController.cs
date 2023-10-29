using Azure;
using BtlNhom6.Data;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using System;
using X.PagedList;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BtlNhom6.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DishController : Controller
    {
        private AuthDbContext db;
        private readonly IHostingEnvironment _environment;

        public DishController(AuthDbContext db, IHostingEnvironment environment)
        {
            _environment = environment;
            this.db = db;
        }
        public IActionResult Index(int? page)
        {
			int pagesize = 6;
			int pagenumber = page == null || page < 0 ? 1 : page.Value;
			var product = db.dishes.AsNoTracking().OrderBy(x => x.DishName);
			PagedList<Dish> lst = new PagedList<Dish>(product, pagenumber, pagesize);
			return View(lst);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, [Bind("DishName,Price,Making,Request,FormFile,FileName,Discount")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName); 
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    dish.NameImage = fileName;
                    db.dishes.Add(dish);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    
        public IActionResult Edit(int id)
        {
            if (id == null || db.dishes == null)
            {
                return NotFound();
            }
            var dish = db.dishes.Find(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewBag.DishID = new SelectList(db.dishes, "DishID", "DishName");
            return View(dish);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("DishID,DishName,Price,Making,Request,Discount,FileName")] Dish dish)
        {
            if (id != dish.DishID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(dish);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //  ViewBag.DishID = new SelectList(db.Dishes, "DishID", "DishName", dish.DishID);
            return View(dish);
        }
        private bool DishExists(int id)
        {
            return (db.dishes?.Any(e => e.DishID == id)).GetValueOrDefault();
        }
        public IActionResult Delete(int id)
        {
            if (id == null || db.dishes == null)
            {
                return NotFound();
            }
            var dish = db.dishes
                .FirstOrDefault(m => m.DishID == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }
        //post:learner/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.dishes == null)
            {
                return Problem("Entity set 'Dishes' is null");
            }
            var dish = db.dishes.Find(id);
            if (dish != null)
            {
                db.dishes.Remove(dish);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
