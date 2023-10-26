using BtlNhom6.Data;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BtlNhom6.Controllers
{
    public class DishController : Controller
    {
        private AuthDbContext db;

        public DishController(AuthDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var dish = db.dishes.ToList();
            return View(dish);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("DishName,Price,Making,Request")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.dishes.Add(dish);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public IActionResult Edit(int id, [Bind("DishID,DishName,Price,Making,Request")] Dish dish)
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
