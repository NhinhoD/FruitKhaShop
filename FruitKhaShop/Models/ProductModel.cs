using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitKhaShop.Models
{
    public class ProductModel
    {
        [Key]
        public string? ProductId { get; set; }
        public string?  ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } 
        public string? ImageUrl { get; set; }
        public double Weight { get; set; }
        // Navigation property
        public ICollection<ProductCategoryModel> ProductCategories { get; set; }

    }
}
