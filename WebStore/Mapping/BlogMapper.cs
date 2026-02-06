using WebStore.Domain.Base;
using WebStore.ViewModels;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebStore.Mapping
{
    public static class BlogMapper
    {
        public static BlogViewModel? ToView(this Blog? b) => b is null ?
            null :
            new BlogViewModel
            {
                Author = b.Author,
                Name = b.Name,
                Rate = b.Rate,
                PictureUrl = b.PictureUrl,
                Block = b.TextBlock,
                publicDate = b.PublicDate,
                SectionID = b.SectionID,
                BrandID = b.BrandID,
                ID = b.ID
            }; 

    }
}
