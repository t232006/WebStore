using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmplController:Controller
    {
        
        public IActionResult Index()
        {
            return View(_employees);
        }
        //[Route("Staff/info/{Id}")]
        [Route("staff/info/{Id}")]  //will be work as default
        [Route("[controller]/info/{Id}")]   //will be work
        public IActionResult Details(int Id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == Id);
            if (employee is null)
                return NotFound();
            else return View(employee);
        }
    }
}
