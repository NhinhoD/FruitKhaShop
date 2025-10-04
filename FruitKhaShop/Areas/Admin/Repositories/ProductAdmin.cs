using FruitKhaShop.Areas.Admin.InterfaceRepositories;
using FruitKhaShop.Areas.ViewModels;
using FruitKhaShop.InterfaceRepositories;
using FruitKhaShop.Models;
using FruitKhaShop.Repository;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
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
        

        public void DeleteProduct(string id)
        {
            throw new NotImplementedException();
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

        public void UpdateProduct(ProductModel product, string id)
        {
            throw new NotImplementedException();
        }
    }
}
