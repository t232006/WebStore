using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Xml.Linq;
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
        public IActionResult InsertEmp() => View("Edit", new EmployeeViewModel());
        public IActionResult Edit(int? Id)
        {
            Employee empl = new Employee();
            if (Id is null)
                return View(new EmployeeViewModel());  
            empl = _employees.GetByID((int)Id);
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
        public IActionResult Edit(EmployeeViewModel? evm)
        {
            
            var empl = new Employee
            {
                Id = evm.Id,
                Name = evm.Name,
                Position = evm.Position,
                DateOfBirth = evm.DateOfBirth,
            };
            if (empl.Id == 0)
            {
                int _id = _employees.Insert(empl);
                return RedirectToAction(nameof(Details), new { Id = _id });
            }
                
            else
                _employees.Edit(empl);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int ID)
        {
            //_employees.Delete(ID);    так нельзя!        
            //return RedirectToAction(nameof(Index));
            Employee? empl = _employees.GetByID(ID);
            if (empl is null) return NotFound();
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
        public IActionResult DeleteConfirmed(int ID)
        {
            if (!_employees.Delete(ID)) return NotFound();
            //_employees.Delete(ID);
            return RedirectToAction(nameof(Index));
        }

    }
}
