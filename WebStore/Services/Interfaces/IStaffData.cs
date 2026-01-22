using WebStore.Domain.Base;

namespace WebStore.Servises.Interfaces
{
    public interface IStaffData<T>
    {
        IEnumerable<T> GetAll();
        T? GetByID(int ID);
        bool Edit(T empl);
        int Insert(T empl);
        bool Delete(int ID);
    }
}
