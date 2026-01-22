using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Domain.Base;
using WebStore.Services.Interfaces;
using WebStore.DAL.Context;

namespace WebStore.Services.InSQL
{
    public class InSQLBlogs : IBlogs
    {
        private readonly WebStoreDB db;

        public InSQLBlogs(WebStoreDB _db)
        {
            db = _db;
        }
        public IEnumerable<Blog> GetBlogs(ProductFilter filter)
        {
            IQueryable<Blog> result = db.Blogs;
            if (filter is not null)
            {
                if (filter.SectionID.HasValue)
                    result = result.Where(r => r.SectionID == filter.SectionID).OrderBy(s=>s.PublicDate);
                if (filter.BrandID is not null)
                    result = result.Where(p => p.BrandID == filter.BrandID).OrderBy(p => p.PublicDate);
            }
            return result;
        }
    }
}
