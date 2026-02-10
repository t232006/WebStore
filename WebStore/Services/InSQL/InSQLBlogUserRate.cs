using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Services.InSQL
{
    public class InSQLBlogUserRate : IBlogUserRate
    {
        private readonly WebStoreDB db;

        public InSQLBlogUserRate(WebStoreDB _db)
        {
            db = _db;
        }
        public decimal AverageRate(int BlogID)
        {
            return db.BlogUserRate.Where(br => br.BlogID == BlogID).Average(r => r.Rate);
        }

        public void Invote(string UserID, int BlogID, byte Rate)
        {
            if (UserVote(UserID, BlogID) is null)
            {
                db.BlogUserRate.Add(new Domain.Base.BlogUserRate
                {
                    UserID = UserID,
                    BlogID = BlogID,
                    Rate = Rate,
                }); 
            }
            else
            {
                var bur = db.BlogUserRate.Where(u => u.UserID == UserID).Where(b => b.BlogID == BlogID).First();
                bur.Rate = Rate;
            }
            db.SaveChanges();
        }

        public byte? UserVote(string? UserID, int BlogID)
        {
            if (UserID == "") return null;
            var uv = db.BlogUserRate.Where(u => u.UserID == UserID).Where(b => b.BlogID == BlogID)
                .Select(r => r.Rate).First();
            return (byte)uv;
        }

        public int VoteNumber(int BlogID)
        {
            return db.BlogUserRate.Where(b => b.BlogID == BlogID).Count();
        }
    }
}
