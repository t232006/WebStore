using WebStore.Models;

namespace WebStore.Servises.Interfaces
{
    public interface IStaffData
    {
        IEnumerable<Employee> GetAll();
        Employee? GetByID(int ID);
        bool Edit(Employee empl);
        int Insert(Employee empl);
        bool Delete(int ID);
    }
}
