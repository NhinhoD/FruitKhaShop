using FruitKhaShop.Areas.ViewModels;
using FruitKhaShop.Models;

namespace FruitKhaShop.Areas.Admin.InterfaceRepositories
{
    public interface IProductAdmin
    {
        ProductModel GetProductById(string id);
        IQueryable<ProductModel> GetAllProduct();
        Task AddProduct(ProductCreateViewModel product);
        void UpdateProduct(ProductModel product, string id);
        void DeleteProduct(string id);
        List<string> GetDistinctProduct();
    }
}
