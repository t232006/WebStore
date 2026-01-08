using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Base;
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
                .OrderBy(b => b.publicDate)
                .Select(b => new BlogViewModel
                {
                    Author = b.Author,
                    Name = b.Name,
                    Rate = b.Rate,
                    PictureUrl=b.PictureUrl, 
                    Block = b.Block,
                    publicDate = b.publicDate,
                    SectionID =b.SectionID,
                    BrandID=b.BrandID
                });
            return View(blogs);
        }
        public IActionResult ShopBlog() => View();    }
    
}
