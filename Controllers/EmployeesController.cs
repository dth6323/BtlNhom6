﻿using BtlNhom6.Data;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BtlNhom6.Controllers
{
    public class Employeescontroller : Controller
    {
        private AuthDbContext db;

        public Employeescontroller(AuthDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            
                var employees = db.Employees.ToList();
                return View(employees);
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeName,Address,Gender,PhoneNumber")]Employee employee)
        {
            if(ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (id == null || db.Employees == null)
                return NotFound();
            var employee = db.Employees.Find(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EmployeeID,EmployeeName,Address,Gender,PhoneNumber")] Employee employee)
        {
            if (id != employee.EmployeeID)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(employee);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExits(employee.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        private bool EmployeeExits(int id)
        {
            return (db.Employees?.Any(e => e.EmployeeID == id)).GetValueOrDefault();
        }
        public IActionResult Delete(int id)
        {
            if (db.Employees == null)
                return Problem("Enity set 'employee' is null");
            var employee = db.Employees.Find(id);
            if (employee != null)
                db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

    }
}