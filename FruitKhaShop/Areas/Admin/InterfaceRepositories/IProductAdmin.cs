using FruitKhaShop.Areas.ViewModels;
using FruitKhaShop.Models;

namespace FruitKhaShop.Areas.Admin.InterfaceRepositories
{
    public interface IProductAdmin
    {
        ProductModel GetProductById(string id);
        IQueryable<ProductModel> GetAllProduct();
        Task AddProduct(ProductCreateViewModel product);
        Task UpdateProduct(ProductCreateViewModel product, string id);
        Task DeleteProduct(string id);
        List<string> GetDistinctProduct();
        List<CategoryModel> GetAllCategories();
        IQueryable<ProductModel> GetAllProductWithCategory();
        ProductCreateViewModel GetProductEditViewModel(string id);
    }
}
