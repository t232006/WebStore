using Microsoft.AspNetCore.Mvc;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;
using WebStore.Domain.Base;

namespace WebStore.Controllers
{
    public class VisitorsController:Controller
    {
        private readonly IStaffData<Visitor, int> _visitors;
        public VisitorsController(IStaffData<Visitor, int> visitors) => _visitors = visitors; 
        public IActionResult Index()
        {
            return View(_visitors.GetAll());
        } 
        public IActionResult Details (int ID)
        {
            Visitor? vis = _visitors.GetByID(ID);
            if (vis is null) return NotFound();
            return View(vis);
        }
        public IActionResult DeleteUser(int ID)
        {
            Visitor? vis = _visitors.GetByID(ID);
            if (vis is null) return NotFound();
            return View(new VisitorsViewModel
            {
                ID = vis.ID,
                password = vis.password,
                Name = vis.Name,
                e_mail = vis.e_mail,
                login = vis.login,
            });
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int ID)
        {
            if (!_visitors.Delete(ID)) return NotFound();
            return RedirectToAction("Index");
        }
        public IActionResult InsertUser()
        {
            return View("EditUser", new VisitorsViewModel());
        }
        public IActionResult EditUserForm(int ID)
        {
            var vis = _visitors.GetByID(ID);
            var vvm = new VisitorsViewModel
            {
                ID = vis.ID,
                password = vis.password,
                Name = vis.Name,
                e_mail = vis.e_mail,
                login = vis.login,
            };
            return View("EditUser", vvm);
        }
        public IActionResult EditUser(VisitorsViewModel vvm)
        {
            if (!ModelState.IsValid) return View();
            var vis = new Visitor
            {
                ID = vvm.ID,
                password = vvm.password,
                Name = vvm.Name,
                e_mail = vvm.e_mail,
                login = vvm.login,
            };
            if (vis.ID == 0)
            {
                vis.regData = DateTime.Today;
                int _ID = _visitors.Insert(vis);
                if (_ID == -1)
                {
                    ModelState.AddModelError("login", "Логин совпадает с уже созданным");
                    return View();
                }
                    
                return RedirectToAction(nameof(Details), new {ID=_ID});
            } else 
            {
                if(_visitors.Edit(vis)==false)
                {
                    ModelState.AddModelError("login", "Логин совпадает с уже созданным");
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }


        }
      

    
    }
}
