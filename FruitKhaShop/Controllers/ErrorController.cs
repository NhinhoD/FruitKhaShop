using Microsoft.AspNetCore.Mvc;

namespace FruitKhaShop.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
