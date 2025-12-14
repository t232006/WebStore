using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, Name = "Alice", Position = "Developer" , DateOfBirth=new DateTime(2002,12,12) },
            new Employee { Id = 2, Name = "Bob", Position = "Designer" , DateOfBirth=new DateTime(2001,11,11) },
            new Employee { Id = 3, Name = "Charlie", Position = "Manager", DateOfBirth=new DateTime(2000,10,10) }
        };
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HelloID(string? ID)
        {
            return Content($"Hello from ID -{ID}");
        }
        public IActionResult Employees()
        {
            return View(_employees);
        }
    }
}
