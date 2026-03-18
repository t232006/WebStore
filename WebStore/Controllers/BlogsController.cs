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
        public IActionResult Index(int? BrandID, int? SectionID, int Page, int PageSize=10) 
        {
            var filter = new ProductFilter{BrandID=BrandID, SectionID=SectionID};
            var blogs = bl.GetBlogs(filter, Page*PageSize, PageSize)
                .OrderBy(b => b.PublicDate)
                .Select(b=>b.ToView(Page*PageSize, Page));
            return View(blogs);
        }
        public IActionResult OneArticle(int BlogID, int Page, int PageSize=10)
        {
            if (BlogID==0) return BadRequest();
            var blog = bl.GetBlogByID(BlogID);
            if (blog is null) return NotFound();
            //ViewBag.blog = blog;
            return View(blog.ToView(Page*PageSize, PageSize));
        }
        [HttpPost]
        public IActionResult PostComment(BlogViewModel bvm)
        {
            if (string.IsNullOrWhiteSpace(bvm.NewCommentText)) return BadRequest();
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            com.WriteComment(bvm.ID,
                    bvm.CommentID,
                    UserID,
                    bvm.NewCommentText);
            return RedirectToAction("OneArticle", "Blogs", new { BlogID = bvm.ID });
        }
        public IActionResult ShopBlog() => View();    }
    
}
