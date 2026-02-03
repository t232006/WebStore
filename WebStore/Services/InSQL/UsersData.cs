using WebStore.DAL.Context;
using WebStore.Domain.Base;
using WebStore.Domain.Identity;
using WebStore.Servises.Interfaces;

namespace WebStore.Services.InSQL
{
    public class UsersData : IStaffData<User, string>
    {
        private readonly WebStoreDB db;
        private readonly ILogger<UsersData> logger;

        public UsersData(WebStoreDB _db, ILogger<UsersData> logger)
        {
            db = _db;
            this.logger = logger;
        }
        public bool Delete(string ID)
        {
            User? emp = db.Users.FirstOrDefault(e => e.UserName == ID);
            if (emp is null)
            {
                logger.LogInformation($"{0} is not found", ID);
                return false;
            }
            else
            {
                db.Users.Remove(emp);
                db.SaveChanges();
            }

            logger.LogInformation($"{0} is deleted", emp);
            return true;
        }

        public bool Edit(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            User? _user = GetByID(user.Id);
            if (_user is null)
            {
                logger.LogWarning("During edit attempt user with ID:{0} - record is not found", user.UserName);
                return false;
            }
            _user.UserName = user.UserName;
            _user.Email = user.Email;
            _user.user_Name = user.user_Name;
            _user.NormalizedUserName = user.UserName.ToUpper();
            //_user.password = user.password;
            db.SaveChanges();
            logger.LogInformation("User {0} is edited", user);
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User? GetByID(string ID)
        {
            return db.Users.FirstOrDefault(v => v.Id == ID); 
        }

        public User? GetByName(string Name) => db.Users.FirstOrDefault(v => v.UserName == Name);

        public string Insert(User user)
        {
            if (user is null) throw new ArgumentNullException();
            if (!db.Users.Contains(user)) 
            {
                db.Users.Add(user);
                db.SaveChanges();
                logger.LogInformation("{0} added", user.ToString()); 
            }
            return db.Users.FirstOrDefault(v=>v==user)!.UserName!;
        }
    }
}
