using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Xml.Linq;
using WebStore.Models;
using WebStore.Services;
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
        //[Route("Staff/info/{ID}")]
        //[Route("staff/info/{ID}")]  //will be work as default
        //[Route("[controller]/info/{ID}")]   //will be work
        public IActionResult Details(int ID)
        {
            var emp = _employees.GetByID(ID);
            if (emp is null) return NotFound();
            return View(emp);
        }
        public IActionResult InsertEmp() => View("Edit", new EmployeeViewModel());
        public IActionResult Edit(int? ID)
        {
            Employee empl = new Employee();
            if (ID is null)
                return View(new EmployeeViewModel());  
            empl = _employees.GetByID((int)ID);
            var evm = new EmployeeViewModel
            {
                ID = empl.ID,
                Name = empl.Name,
                Position = empl.Position,
                DateOfBirth = empl.DateOfBirth,
            }; 
            return View(evm);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel? evm)
        {
           /* if ((DateTime.Today - evm.DateOfBirth).TotalDays / 365 < 18)
                ModelState.AddModelError("DateOfBirth", "Слишком молод для работы здесь");*/
            if (!ModelState.IsValid)
                return View();
            var empl = new Employee
            {
                ID = evm.ID,
                Name = evm.Name,
                Position = evm.Position,
                DateOfBirth = evm.DateOfBirth,
            };
            if (empl.ID == 0)
            {
                int _id = _employees.Insert(empl);
                return RedirectToAction(nameof(Details), new { ID = _id });
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
                ID = empl.ID,
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
