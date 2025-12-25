using WebStore.Data;
using WebStore.Models;
using WebStore.Servises.Interfaces;

namespace WebStore.Servises
{
    public class InMemoryStaffData : IStaffData
    {
        private readonly ICollection<Employee> staff;
        private readonly ILogger<InMemoryStaffData> logger;
        private int LastId;

        InMemoryStaffData(ILogger<InMemoryStaffData> _logger)
        {
            this.logger = _logger;
            staff = TestData._employees;
            if (staff.Count > 0)
                LastId = staff.Max(t => t.Id) + 1;
            else LastId = 1;
        }
        public bool Delete(int Id)
        {
            Employee? empl = staff.FirstOrDefault(t => t.Id == Id);
            if (empl is null) return false;
            staff.Remove(empl);
            return true;
        }

        public bool Edit(Employee empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(empl));
            Employee? _empl = GetByID(empl.Id);
            if (_empl is null) return false;
            _empl.Name = empl.Name;
            _empl.DateOfBirth = empl.DateOfBirth;
            _empl.Position = empl.Position;
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return staff;
        }

        public Employee? GetByID(int Id)
        {
            //return (Employee?)staff.Select(t => t.Id);
            return staff.FirstOrDefault(t => t.Id == Id);
        }

        public int Insert(Employee empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(Employee));
            if (staff.Contains(empl)) return empl.Id;
            empl.Id = LastId++;
            staff.Add(empl);
            return empl.Id;
        }
    }
}
