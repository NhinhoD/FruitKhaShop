using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FruitKhaShop.Models;
using FruitKhaShop.Repository;
using FruitKhaShop.Areas.Admin.InterfaceRepositories;

namespace FruitKhaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class CategoryModelsAdminController(ICategoryAdmin categoryAdmin) : Controller
    {
        private readonly ICategoryAdmin _categoryAdmin = categoryAdmin;

        // GET: Admin/CategoryModelsAdmin
        public IActionResult Index()
        {
            return  View(_categoryAdmin.GetAllCategory().ToList());
        }

        // GET: Admin/CategoryModelsAdmin/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = _categoryAdmin.GetCategoryById(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: Admin/CategoryModelsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryModelsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                _categoryAdmin.AddCategory(categoryModel);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Admin/CategoryModelsAdmin/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = _categoryAdmin.GetCategoryById(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        // POST: Admin/CategoryModelsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("CategoryId,CategoryName")] CategoryModel categoryModel)
        {
            if (id != categoryModel.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryAdmin.UpdateCategory(categoryModel, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Admin/CategoryModelsAdmin/Delete/5

        // POST: Admin/CategoryModelsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public IActionResult DeleteConfirmed(string id)     
        { 
            _categoryAdmin.DeleteCategory(id);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
