using WebStore.Data;
using WebStore.Domain.Base;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryBlogs : IBlogs
    {
        public IEnumerable<Blog> GetBlogs()
        {
            return TestData.Blogs;
        }
   
    }
}
