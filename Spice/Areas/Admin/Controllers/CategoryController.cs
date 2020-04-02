using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _db.Categories.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create() {

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category) 
        {

            if (ModelState.IsValid)
            {
              await  _db.Categories.AddAsync(category);
               await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else {

                return View(category);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else {

                var category =await _db.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                else {

                    return View(category);
                }
                
            }


        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category) {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else {
                return View(category);
            }

        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
             var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }




        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return View();
            }
            else
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

        }



        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Categories.FindAsync(id);
            if (category != null)
                return View(category);

            return NotFound();
        }








    }
}