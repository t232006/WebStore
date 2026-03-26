using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Xml.Linq;
using WebStore.Mapping;
using WebStore.Services;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;
using WebStore.Domain.Base;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Identity;

namespace WebStore.Controllers
{
    [Authorize]
    public class EmplController:Controller
    {
        private readonly IStaffData<Employee, int> _employees;
        public EmplController(IStaffData<Employee, int> empl) => _employees = empl;
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
        [Authorize(Roles =Role.Administrators)]
        public IActionResult Edit(int? ID)
        {
            Employee empl = new Employee();
            if (ID is null)
                return View(new EmployeeViewModel());  
            empl = _employees.GetByID((int)ID)!;
            var evm = empl.ToView();
            return View(evm);
        }
        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Edit(EmployeeViewModel? evm)
        {
           /* if ((DateTime.Today - evm.DateOfBirth).TotalDays / 365 < 18)
                ModelState.AddModelError("DateOfBirth", "Слишком молод для работы здесь");*/
            if (!ModelState.IsValid)
                return View();
            var empl = evm.FromView();
            if (empl.ID == 0)
            {
                int _id = _employees.Insert(empl);
                return RedirectToAction(nameof(Details), new { ID = _id });
            }
                
            else
                _employees.Edit(empl);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = Role.Administrators)]
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
        [Authorize(Roles =Role.Administrators)]
        public IActionResult DeleteConfirmed(int ID)
        {
            if (!_employees.Delete(ID)) return NotFound();
            //_employees.Delete(ID);
            return RedirectToAction(nameof(Index));
        }

    }
}
