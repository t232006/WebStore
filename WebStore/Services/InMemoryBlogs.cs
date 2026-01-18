using WebStore.Data;
using WebStore.Domain.Base;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryBlogs : IBlogs
    {
        public IEnumerable<Blog> GetBlogs(ProductFilter filter)
        {
            var result = TestData.Blogs;
            if (filter is not null)
                {
                if (filter.SectionID is not null)
                    result = result.Where(r => r.SectionID == filter.SectionID);
                if (filter.BrandID is not null)
                    result = result.Where(r => r.BrandID == filter.BrandID);
                }
            return result;
        }
   
    }
}
