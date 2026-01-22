using WebStore.Data;
using WebStore.Domain.Base;
using WebStore.Servises.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryVisitorsData : IStaffData<Visitor>
    {
        private readonly ICollection<Visitor> VisitorsList;
        int LastID;
        private readonly ILogger<InMemoryStaffData> logger;

        public InMemoryVisitorsData(ILogger<InMemoryStaffData> _logger)
        {
            VisitorsList = TestData._visitors;
            LastID = VisitorsList.Max(v => v.ID)+1;
            logger = _logger;
        }
        public bool Delete(int ID)
        {
            Visitor? vis = VisitorsList.FirstOrDefault(v => v.ID == ID);
            if (vis is null) return false; else
                VisitorsList.Remove(vis);
            logger.LogInformation("User with ID:{0} is deleted", ID);
            return true;
        }
        public Visitor? GetByID(int ID)
        {
            return VisitorsList.FirstOrDefault(v => v.ID == ID);
        }
        public bool Edit(Visitor empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(empl));
            Visitor? vis = GetByID(empl.ID);
            if (vis is null) return false;
            if (VisitorsList.FirstOrDefault(v => v.login == empl.login) is not null)
            {
                logger.LogError("User with ID:{0} edited login which already exists", empl.ID);
                return false;
            }
            vis.Name = empl.Name;
            vis.e_mail = empl.e_mail;
            vis.regData = empl.regData;
            logger.LogInformation("User with ID:{0} is edited", empl.ID);
            return true;
        }
        public int Insert(Visitor empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(empl));
            if (VisitorsList.Contains(empl)) return empl.ID;
            if (VisitorsList.FirstOrDefault(v => v.login == empl.login) is not null)
                return -1;
            empl.ID = LastID;
            VisitorsList.Add(empl);
            return LastID++; 
        }
        IEnumerable<Visitor> IStaffData<Visitor>.GetAll()
        {
            return VisitorsList;
        }
    }
}
