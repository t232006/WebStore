using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index([FromServices] IProductData productData)
        {
            var products = productData.GetProducts()
                .OrderBy(p => p.Order)
                .Take(6)
                .Select(p => new ProductViewModel
                {
                    ID = p.ID,
                    Name = p.Name,
                    Price = p.Price,
                    PictureUrl = p.ImageUrl
                });
            ViewBag.Products = products;

            return View();
        }
        public IActionResult HelloID(string? ID)
        {
            return Content($"Hello from ID -{ID}");
        }
        public IActionResult Error404() => View();
    }
}
