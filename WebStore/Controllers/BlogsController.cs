using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BlogsController:Controller
    {
        private readonly IBlogs bl;

        public BlogsController(IBlogs BL) => bl = BL;
        public IActionResult Index() 
        {
            var blogs = bl.GetBlogs()
                .OrderBy(b => b.publicDate)
                .Select(b => new BlogViewModel
                {
                    Author = b.Author,
                    Name = b.Name,
                    Rate = b.Rate,
                    PictureUrl=b.PictureUrl, 
                    Block = b.Block,
                    publicDate = b.publicDate
                });
            return View(blogs);
        }
        public IActionResult ShopBlog() => View();    }
    
}
