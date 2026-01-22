using WebStore.DAL.Context;
using WebStore.Domain.Base;
using WebStore.Servises.Interfaces;

namespace WebStore.Services.InSQL
{
    public class InSQLVisitorsData : IStaffData<Visitor>
    {
        private readonly WebStoreDB db;
        private readonly ILogger<InSQLVisitorsData> logger;

        public InSQLVisitorsData(WebStoreDB _db, ILogger<InSQLVisitorsData> logger)
        {
            db = _db;
            this.logger = logger;
        }
        public bool Delete(int ID)
        {
            Visitor? emp = db.Visitors.FirstOrDefault(e => e.ID == ID);
            if (emp is null)
            {
                logger.LogInformation($"{0} is not found", ID);
                return false;
            }
            else
            {
                db.Visitors.Remove(emp);
                db.SaveChanges();
            }

            logger.LogInformation($"{0} is deleted", emp);
            return true;
        }

        public bool Edit(Visitor empl)
        {
            if (empl is null) throw new ArgumentNullException(nameof(empl));
            Visitor? _empl = GetByID(empl.ID);
            if (_empl is null)
            {
                logger.LogWarning("During edit attempt visitor with ID:{0} - record is not found", empl.ID);
                return false;
            }
            _empl.Name = empl.Name;
            _empl.e_mail = empl.e_mail;
            _empl.password = empl.password;
            db.SaveChanges();
            logger.LogInformation("Visitor {0} is edited", empl);
            return true;
        }

        public IEnumerable<Visitor> GetAll()
        {
            return db.Visitors;
        }

        public Visitor? GetByID(int ID)
        {
            return db.Visitors.FirstOrDefault(v => v.ID == ID); 
        }

        public int Insert(Visitor empl)
        {
            if (empl is null) throw new ArgumentNullException();
            if (db.Visitors.Contains(empl)) return db.Visitors.FirstOrDefault(v => v==empl).ID;
            db.Visitors.Add(empl);
            db.SaveChanges();
            logger.LogInformation("{0} added", empl.ToString());
            return db.Visitors.Max(v => v.ID);
        }
    }
}
