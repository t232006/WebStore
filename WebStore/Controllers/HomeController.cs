using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HelloID(string? ID)
        {
            return Content($"Hello from ID -{ID}");
        }
        
    }
}
