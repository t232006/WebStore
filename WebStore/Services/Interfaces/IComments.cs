using WebStore.Domain.Base;

namespace WebStore.Services.Interfaces
{
    public interface IComments
    {
        IEnumerable<Comment>? GetComments(int? BlogID, int? CommentID);
        int WriteComment(string Author, string Text);
        Comment? GetCommentByID(int ID);
    }
}
