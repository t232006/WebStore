using WebStore.Domain.Base;
using WebStore.ViewModels;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebStore.Mapping
{
    public static class BlogMapper
    {
        public static BlogViewModel? ToView(this Blog? b, int Skip, int Take)
        {
            if (b is null) return null;
            IEnumerable<CommentViewModel> _Comments;
            if (Take == 0) _Comments = Enumerable.Empty<CommentViewModel>(); 
                else
            _Comments = b.Comments.Select(c => new CommentViewModel
             {
                 Text = c.Text,
                 Author = c.Author,
                 ID = c.ID,
                 PublicDate = c.PublicDate,
                 ParentCommentID = c.CommentID
             });
            if (Skip > 0)
            {
                if (Skip > _Comments.Count()) _Comments = Enumerable.Empty<CommentViewModel>(); 
                    else
                _Comments = _Comments.Skip(Skip);  
            }
            return new BlogViewModel
            {
                Author = b.Author,
                Name = b.Name,
                PictureUrl = b.PictureUrl,
                Block = b.TextBlock,
                publicDate = b.PublicDate,
                SectionID = b.SectionID,
                BrandID = b.BrandID,
                Comments = _Comments.Take(Take).ToList(),
                ID = b.ID
            };
        }
            
             

    }
}
