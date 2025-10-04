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
            return View(_productAdmin.GetAllProduct().ToList());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductModelsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Price,ImageUrl,Quantity,Category,Weight")] ProductModel productModel)
        {
            return View(productModel);
        }

        // GET: Admin/ProductModelsAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            return View(await Task.FromResult(_productAdmin.GetProductById(id)));
        }

        // POST: Admin/ProductModelsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductId,ProductName,Description,Price,ImageUrl,Quantity,Category,Weight")] ProductModel productModel)
        {
            if (id != productModel.ProductId)
            {
                return NotFound();
            }

            
            return View(productModel);
        }

        // GET: Admin/ProductModelsAdmin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            return View();
        }

        // POST: Admin/ProductModelsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
           
            return RedirectToAction(nameof(Index));
        }

    }
}
