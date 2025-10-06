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
using FruitKhaShop.Areas.ViewModels;

namespace FruitKhaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductModelsAdminController : Controller
    {
        private readonly IProductAdmin _productAdmin;

        public ProductModelsAdminController(IProductAdmin productAdmin)
        {
            _productAdmin = productAdmin;
        }

        // GET: Admin/ProductModelsAdmin
        public IActionResult Index()
        {
            var products = _productAdmin.GetAllProductWithCategory().ToList();
            return View(products);
        }


        // GET: Admin/ProductModelsAdmin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await Task.FromResult(_productAdmin.GetProductById(id)));
        }

        // GET: Admin/ProductModelsAdmin/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreateViewModel
            {
                Categories = _productAdmin.GetAllCategories(), // ✅ Lấy dữ liệu từ DI
                SelectedCategoryIds = new List<string>()
            };
            return View(model);
        }

        // POST: Admin/ProductModelsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _productAdmin.GetAllCategories();
                return View(model);
            }

            await _productAdmin.AddProduct(model); 

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ProductModelsAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var vm = _productAdmin.GetProductEditViewModel(id);
            if (vm == null) return NotFound();

            // ✅ Lấy ảnh cũ để hiển thị trên View
            var product = _productAdmin.GetProductById(id);
            ViewBag.ExistingImage = product?.ImageUrl;

            return View(await Task.FromResult(vm));
        }

        // POST: Admin/ProductModelsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductCreateViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _productAdmin.GetAllCategories();
                return View(model);
            }
            await _productAdmin.UpdateProduct(model, id);
            return RedirectToAction(nameof(Index));
        }

       

        // POST: Admin/ProductModelsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            try
            {
                await _productAdmin.DeleteProduct(id);

                TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công!";
                TempData["Source"] = "Product"; 
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Không thể xóa sản phẩm: " + ex.Message;
                TempData["Source"] = "Product";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
