using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Base;
using WebStore.Servises.Interfaces;

namespace WebStore.Services.InSQL
{
    public class InSQLStaffData : IStaffData<Employee, int>
    {
        private readonly WebStoreDB db;
        private readonly ILogger<InSQLStaffData> logger;

        public InSQLStaffData(WebStoreDB _db, ILogger<InSQLStaffData> _logger)
        {
            this.db = _db;
            logger = _logger;
        }
        public bool Delete(int ID)
        {

            Employee? emp = db.Employees.FirstOrDefault(e => e.ID==ID);
            if (emp is null)
            {
                logger.LogInformation($"{0} is not found", ID);
                return false;
            }
            else
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
            } 
                 
            logger.LogInformation($"{0} is deleted", emp);
            return true;
        }

        public bool Edit(Employee empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(empl));
            Employee? _empl = GetByID(empl.ID);
            if (_empl is null)
            {
                logger.LogWarning("During edit attempt employee with ID:{0} - record is not found", empl.ID);
                return false;
            }
            _empl.Name = empl.Name;
            _empl.DateOfBirth = empl.DateOfBirth;
            _empl.Position = empl.Position;
            db.SaveChanges();
            logger.LogInformation("Employee {0} is edited", empl);
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public Employee? GetByID(int ID)
        {
            return db.Employees.FirstOrDefault(e => e.ID == ID);
        }


        public int Insert(Employee empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(Employee));
            if (db.Employees.Contains(empl)) return db.Employees.FirstOrDefault(e => e == empl).ID;
            else
            { 
                db.Employees.Add(empl);
                db.SaveChanges();
            } 
            logger.LogInformation($"{0} is added",empl);
            return db.Employees.Max(e => e.ID);
        }
    }
}
