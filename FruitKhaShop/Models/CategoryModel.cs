using System.ComponentModel.DataAnnotations;

namespace FruitKhaShop.Models
{
    public class CategoryModel
    {
        [Key]
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        // Navigation property
        public ICollection<ProductCategoryModel>? ProductCategories { get; set; } = new List<ProductCategoryModel>();
    }
}
