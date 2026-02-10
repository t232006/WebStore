using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Controllers
{
    public class BlogUserController :Controller
    {
        private readonly IBlogUserRate bur;

        public BlogUserController(IBlogUserRate _bur)
        {
            bur = _bur;
        }
        public IActionResult Invote(string UserID, int BlogID, byte Rate)
        {
            bur.Invote(UserID, BlogID, Rate);
            return RedirectToAction("OneArticle", "Blogs", new {BlogID=BlogID});
        }
    }
}
