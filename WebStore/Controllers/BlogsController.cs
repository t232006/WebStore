using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Base;
using WebStore.Mapping;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BlogsController:Controller
    {
        private readonly IBlogs bl;
        public BlogsController(IBlogs BL) => bl = BL;
        public IActionResult Index(int? BrandID, int? SectionID) 
        {
            var filter = new ProductFilter{BrandID=BrandID, SectionID=SectionID};
            var blogs = bl.GetBlogs(filter)
                .OrderBy(b => b.PublicDate)
                .Select(b=>b.ToView());
            return View(blogs);
        }
        public IActionResult OneArticle(int BlogID)
        {
            if (BlogID==0) return BadRequest();
            var blog = bl.GetBlogByID(BlogID);
            if (blog is null) return NotFound();
            //ViewBag.blog = blog;
            return View(blog.ToView());
        }
        public IActionResult ShopBlog() => View();    }
    
}
