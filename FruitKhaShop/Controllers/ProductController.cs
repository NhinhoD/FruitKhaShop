using Microsoft.AspNetCore.Mvc;

namespace FruitKhaShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //detail product
        public IActionResult Detail()
        {
            return View();
        }
    }
}
