using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Domain.Base;
using WebStore.Services.Interfaces;
using WebStore.DAL.Context;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebStore.Services.InSQL
{
    public class InSQLBlogs : IBlogs
    {
        private readonly WebStoreDB db;

        public InSQLBlogs(WebStoreDB _db)
        {
            db = _db;
        }

        public Blog? GetBlogByID(int ID)
        {
            return db.Blogs.FirstOrDefault(b => b.ID == ID)  ?? null;
        }

        public IEnumerable<Blog> GetBlogs(ProductFilter filter)
        {
            IQueryable<Blog> result = db.Blogs.Include(b=>b.Author);
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
