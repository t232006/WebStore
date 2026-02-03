using WebStore.Data;
//using WebStore.Models;
using WebStore.Servises.Interfaces;
using Employee = WebStore.Domain.Base.Employee;

namespace WebStore.Services.InMemory
{
    public class InMemoryStaffData : IStaffData<Employee, int>
    {
        private readonly ICollection<Employee> staff;
        private readonly ILogger<InMemoryStaffData> logger;
        private int LastId;

        public InMemoryStaffData(ILogger<InMemoryStaffData> _logger)
        {
            this.logger = _logger;
            staff = TestData._employees;
            if (staff.Count > 0)
                LastId = staff.Max(t => t.ID) + 1;
            else LastId = 1;
        }
        public bool Delete(int ID)
        {
            Employee? empl = staff.FirstOrDefault(t => t.ID == ID);
            if (empl is null)
            {
                logger.LogWarning("During delete attempt employee with ID:{0} - record is not found", ID);
                return false;
            }
            staff.Remove(empl);
            logger.LogInformation("Employee with ID:{0} is deleted", ID);
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
            logger.LogInformation("Employee {0} is edited", empl);
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return staff;
        }

        public Employee? GetByID(int ID)
        {
            //return (Employee?)staff.Select(t => t.ID);
            return staff.FirstOrDefault(t => t.ID == ID);
        }

        public Employee? GetByName(int Name)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(Employee));
            if (staff.Contains(empl)) return empl.ID;
            empl.ID = LastId++;
            staff.Add(empl);
            logger.LogInformation("Employee {0} is inserted", empl);
            return empl.ID;
        }
    }
}
