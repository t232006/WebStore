using WebStore.Domain.Base;

namespace WebStore.Servises.Interfaces
{
    public interface IStaffData<T1,T2>
    {
        IEnumerable<T1> GetAll();
        T1? GetByID(T2 ID);
        bool Edit(T1 empl);
        T2 Insert(T1 empl);
        bool Delete(T2 ID);
    }
}
