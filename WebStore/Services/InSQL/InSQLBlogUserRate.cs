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

        public byte UserVote(string UserID, int BlogID)
        {
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
