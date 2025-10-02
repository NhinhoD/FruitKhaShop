using Microsoft.AspNetCore.Mvc;

namespace FruitKhaShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
