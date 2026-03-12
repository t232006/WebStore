using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using WebStore.Domain.Base;
using WebStore.Mapping;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BlogsController:Controller
    {
        private readonly IBlogs bl;
        private readonly IComments com;

        public BlogsController(IBlogs BL, IComments _com) 
        {
            bl = BL; com = _com;
        }
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
        [HttpPost]
        public IActionResult PostBlogComment(BlogViewModel bvm)
        {
            string UserID=User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            com.WriteComment(bvm.ID, 
                    null, 
                    UserID, 
                    bvm.NewCommentText);
            return RedirectToAction("OneArticle", "Blogs", new { BlogID = bvm.ID });
        }
        [HttpPost]
        public IActionResult PostCommentComment(BlogViewModel bvm)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            com.WriteComment(null,
                    bvm.CommentID,
                    UserID,
                    bvm.NewCommentText);
            return RedirectToAction("OneArticle", "Blogs", new { BlogID = bvm.ID });
        }
        public IActionResult ShopBlog() => View();    }
    
}
