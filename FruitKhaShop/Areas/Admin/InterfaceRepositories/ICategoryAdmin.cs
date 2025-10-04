using FruitKhaShop.Models;

namespace FruitKhaShop.Areas.Admin.InterfaceRepositories
{
    public interface ICategoryAdmin
    {
        CategoryModel GetCategoryById(string id);
        IQueryable<CategoryModel> GetAllCategory();
        void AddCategory(CategoryModel category);
        void UpdateCategory(CategoryModel loai, string id);
        void DeleteCategory(string Id);
        List<string> GetDistinctCategory();
    }
}
