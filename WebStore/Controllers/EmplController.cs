using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Servises;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

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
        //[Route("staff/info/{Id}")]  //will be work as default
        //[Route("[controller]/info/{Id}")]   //will be work
        public IActionResult Details(int Id)
        {
            var emp = _employees.GetByID(Id);
            if (emp is null) return NotFound();
            return View(emp);
        }
        public IActionResult Edit(int Id)
        {
            Employee? empl = _employees.GetByID(Id);
            var evm = new EmployeeViewModel
            {
                Id = empl.Id,
                Name = empl.Name,
                Position = empl.Position,
                DateOfBirth = empl.DateOfBirth,
            };
            return View(evm);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel evm)
        {
            var empl = new Employee
            {
                Id = evm.Id,
                Name = evm.Name,
                Position = evm.Position,
                DateOfBirth = evm.DateOfBirth,
            };
            _employees.Edit(empl);
            return RedirectToAction(nameof(Index));
        }
    }
}
