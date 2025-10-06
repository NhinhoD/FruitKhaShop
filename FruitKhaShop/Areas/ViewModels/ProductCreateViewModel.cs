using FruitKhaShop.Models;
using System.ComponentModel.DataAnnotations;

namespace FruitKhaShop.Areas.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
       
        public IFormFile? ImageUrl { get; set; }
        public string? ExistingImageUrl { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Weight { get; set; }

        // Danh mục (nhiều–nhiều)
        [Required]
        public List<string>? SelectedCategoryIds { get; set; } = new List<string>();

        // Danh sách danh mục hiển thị (dùng trong view)
        [Required]
        public List<CategoryModel>? Categories { get; set; } = new List<CategoryModel>();
    }
}
