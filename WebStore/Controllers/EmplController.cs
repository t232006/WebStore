using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Servises;
using WebStore.Servises.Interfaces;

namespace WebStore.Controllers
{
    public class EmplController:Controller
    {
        private readonly IStaffData _employees;
        public EmplController(IStaffData empl) => _employees = empl;
        public IActionResult Index()
        {
            return View(_employees.GetAll());
        }
        //[Route("Staff/info/{Id}")]
        [Route("staff/info/{Id}")]  //will be work as default
        [Route("[controller]/info/{Id}")]   //will be work
        public IActionResult Details(int Id)
        {
            var emp = _employees.GetByID(Id);
            if (emp is null) return NotFound();
            return View(emp);
        }
    }
}
