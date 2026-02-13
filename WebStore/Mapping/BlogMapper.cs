using WebStore.Domain.Base;
using WebStore.ViewModels;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebStore.Mapping
{
    public static class BlogMapper
    {
        public static BlogViewModel? ToView(this Blog? b)
        {
            if (b is null) return null;
            
            return new BlogViewModel
            {
                Author = b.Author,
                Name = b.Name,
                PictureUrl = b.PictureUrl,
                Block = b.TextBlock,
                publicDate = b.PublicDate,
                SectionID = b.SectionID,
                BrandID = b.BrandID,
                Comments = b.Comments.Select(c=>new CommentViewModel
                {
                    Text = c.Text,
                    Author = c.Author,
                    ID = c.ID,
                    PublicDate = c.PublicDate
                }).ToList(),
                ID = b.ID
            };
        }
            
             

    }
}
