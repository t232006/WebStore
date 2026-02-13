using WebStore.DAL.Context;
using WebStore.Domain.Base;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class InSQLComments : IComments
    {
        private readonly WebStoreDB db;

        public InSQLComments (WebStoreDB _db)
        {
            db = _db;
        }

        public Comment? GetCommentByID(int ID)
        {
            return db.Comments.FirstOrDefault(b => b.ID == ID) ?? null;
        }

        public IEnumerable<Comment>? GetComments(int? BlogID, int? CommentID)
        {
            if ((BlogID is null) && (CommentID is null)) return null;
            if (CommentID.HasValue) return db.Comments.Where(c => c.CommentID == CommentID);
            return db.Comments.Where(b=>b.BlogID == BlogID);
        }

        public int WriteComment(int? BlogID, int? CommentID, string Author, string Text)
        {
            var author = db.Users.FirstOrDefault(u => u.Id == Author);
            db.Comments.Add(new Comment { Author = author!, 
                                        Text = Text,
                                        BlogID = BlogID,
                                        CommentID=CommentID});
            db.SaveChanges();
            return db.Comments.OrderBy(u => u.ID).Select(u => u.ID).Last();
        }
    }
}
