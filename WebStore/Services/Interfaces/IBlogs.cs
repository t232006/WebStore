using WebStore.Domain.Base;
namespace WebStore.Services.Interfaces
{
    public interface IBlogs
    {
        IEnumerable<Blog> GetBlogs(ProductFilter filter);
    }
}
