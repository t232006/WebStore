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
            var RatesList = db.BlogUserRate.Where(br => br.BlogID == BlogID);
            if (RatesList.Any())
                return RatesList.Average(r => r.Rate);
            return 0;
        }

        public void Invote(string UserID, int BlogID, byte Rate)
        {
            if (UserVote(UserID, BlogID) is null||(UserVote(UserID, BlogID)==0))
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
            var uv = db.BlogUserRate.Where(u => u.UserID == UserID).Where(b => b.BlogID == BlogID);
            if (uv.Any())
            return  (byte)uv.Select(r => r.Rate).First();
            return 0;
        }

        public int VoteNumber(int BlogID)
        {
            return db.BlogUserRate.Where(b => b.BlogID == BlogID).Count();
        }
    }
}
