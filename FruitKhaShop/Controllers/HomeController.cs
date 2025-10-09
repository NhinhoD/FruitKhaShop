using System.Diagnostics;
using FruitKhaShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitKhaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    ViewData["ErrorMessage"] = "Trang bạn yêu cầu không tồn tại.";
                    ViewData["Title"] = "404 - Not Found";
                }
                else if (statusCode == 500)
                {
                    ViewData["ErrorMessage"] = "Đã xảy ra lỗi hệ thống.";
                    ViewData["Title"] = "Lỗi máy chủ";
                }
                else
                {
                    ViewData["ErrorMessage"] = $"Đã xảy ra lỗi {statusCode}";
                    ViewData["Title"] = "Lỗi";
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Đã xảy ra lỗi không xác định.";
                ViewData["Title"] = "Lỗi";
            }

            return View("Error");
        }


        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
