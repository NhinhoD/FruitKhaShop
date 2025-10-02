namespace FruitKhaShop.Models
{
    public class ProductCategoryModel
    {
        public string ProductId { get; set; }
        public ProductModel Product { get; set; }

        public string CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
