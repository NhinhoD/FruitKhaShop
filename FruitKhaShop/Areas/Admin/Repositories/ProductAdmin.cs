using FruitKhaShop.Areas.Admin.InterfaceRepositories;
using FruitKhaShop.Areas.ViewModels;
using FruitKhaShop.InterfaceRepositories;
using FruitKhaShop.Models;
using FruitKhaShop.Repository;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace FruitKhaShop.Areas.Admin.Repositories
{
    public class ProductAdmin : IProductAdmin
    {
        private readonly DataContext _context;
        private readonly IPhotoService _photoService;
        private readonly IWebHostEnvironment _env;
        public ProductAdmin(DataContext context, IPhotoService photoService, IWebHostEnvironment env)
        {
            _context = context;
            _photoService = photoService;
            _env = env;
        }
        public async Task AddProduct(ProductCreateViewModel model)
        {
            // Tạo sản phẩm mới
            var newProduct = new ProductModel
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = model.ProductName!,
                Description = model.Description!,
                Price = model.Price,
                //Category = string.Join(", ", model.SelectedCategoryIds ?? new List<string>()),
                Quantity = model.Quantity,
                Weight = model.Weight
            };

            // Upload ảnh bằng Cloudinary
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                var (imageUrl, publicId) = await _photoService.UploadImageAsync(model.ImageUrl, "Products");
                newProduct.ImageUrl = imageUrl;
            }
            // Gán mối quan hệ nhiều–nhiều
            if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any())
            {
                newProduct.ProductCategories = model.SelectedCategoryIds.Select(cid => new ProductCategoryModel
                {
                    ProductId = newProduct.ProductId,
                    CategoryId = cid
                }).ToList();
            }

            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }
        

        public async Task DeleteProduct(string id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var publicId = Path.GetFileNameWithoutExtension(new Uri(product.ImageUrl).AbsolutePath);
                await _photoService.DeleteImageAsync("Products/" + publicId);
            }
            var productCategories = _context.ProductCategories
       .Where(pc => pc.ProductId == id);
            _context.ProductCategories.RemoveRange(productCategories);
            _context.Products.Remove(product!);
          await  _context.SaveChangesAsync();
        }

        public IQueryable<ProductModel> GetAllProduct()
        {
            var product = _context.Products.ToList().AsQueryable();
            return product;
        }

        public List<string> GetDistinctProduct()
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductById(string id)
        {
            ProductModel ProductId = _context.Products.FirstOrDefault(x => x.ProductId == id)!;
            return ProductId;
        }

        public async Task UpdateProduct(ProductCreateViewModel model, string id)
        {
            var existingProduct = _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefault(p => p.ProductId == id);

            if (existingProduct == null)
                throw new Exception("Product not found");

            // Cập nhật thông tin cơ bản
            existingProduct.ProductName = model.ProductName!;
            existingProduct.Description = model.Description!;
            existingProduct.Price = model.Price;
            existingProduct.Quantity = model.Quantity;
            existingProduct.Weight = model.Weight;

            // ✅ Nếu có ảnh mới -> cập nhật
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                // Xóa ảnh cũ
                if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                {
                    var publicId = Path.GetFileNameWithoutExtension(new Uri(existingProduct.ImageUrl).AbsolutePath);
                    await _photoService.DeleteImageAsync("Products/" + publicId);
                }

                // Upload ảnh mới
                var (imageUrl, publicIdNew) = await _photoService.UploadImageAsync(model.ImageUrl, "Products");
                if (!string.IsNullOrEmpty(imageUrl))
                    existingProduct.ImageUrl = imageUrl;
            }
            else
            {
                // ✅ Nếu không chọn ảnh mới → giữ ảnh cũ
                existingProduct.ImageUrl = model.ExistingImageUrl;
            }

            // ✅ Cập nhật danh mục
            if (model.SelectedCategoryIds != null)
            {
                _context.ProductCategories.RemoveRange(existingProduct.ProductCategories);

                existingProduct.ProductCategories = model.SelectedCategoryIds.Select(cid => new ProductCategoryModel
                {
                    ProductId = existingProduct.ProductId,
                    CategoryId = cid
                }).ToList();
            }

            _context.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        public List<CategoryModel> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public IQueryable<ProductModel> GetAllProductWithCategory()
        {
            return _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category);
        }
        public ProductCreateViewModel GetProductEditViewModel(string id)
        {
            var product = _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return null!;

            var vm = new ProductCreateViewModel
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Weight = product.Weight,
                Categories = _context.Categories.ToList(),
                SelectedCategoryIds = product.ProductCategories?.Select(pc => pc.CategoryId).ToList()
            };

            return vm;
        }

    }
}
