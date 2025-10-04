using FruitKhaShop.Areas.Admin.InterfaceRepositories;
using FruitKhaShop.Models;
using FruitKhaShop.Repository;

namespace FruitKhaShop.Areas.Admin.Repositories
{
    public class CategoryAdmin : ICategoryAdmin
    {
        DataContext _context;

        public CategoryAdmin(DataContext context)
        {
            _context = context;
        }
        public void AddCategory(CategoryModel category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            // Sinh ID duy nhất
            category.CategoryId = Guid.NewGuid().ToString("N"); // "N" bỏ dấu gạch ngang

            _context.Categories.Add(category);
            _context.SaveChanges();
        }


        public void DeleteCategory(string Id)
        {
            CategoryModel category = _context.Categories
                            .FirstOrDefault(l => l.CategoryId == Id)!;

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();

            }
        }

        public IQueryable<CategoryModel> GetAllCategory()
        {
            var category = _context.Categories
            .Select(category => new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            });

            return category;
        }

        public CategoryModel GetCategoryById(string id)
        {
            CategoryModel category = _context.Categories.FirstOrDefault(x => x.CategoryId == id)!;
            return category;
        }

        public List<string> GetDistinctCategory()
        {
            return _context.Categories.Select(l => l.CategoryName).Distinct().ToList()!;
        }

        public void UpdateCategory(CategoryModel loai, string id)
        {
            var existingCategory = _context.Categories.Find(id);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = loai.CategoryName;

                _context.SaveChanges();
            }
        }
    }
}
