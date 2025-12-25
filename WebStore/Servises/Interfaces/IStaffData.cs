using WebStore.Models;

namespace WebStore.Servises.Interfaces
{
    public interface IStaffData
    {
        IEnumerable<Employee> GetAll();
        Employee? GetByID(int Id);
        bool Edit(Employee empl);
        int Insert(Employee empl);
        bool Delete(int Id);
    }
}
