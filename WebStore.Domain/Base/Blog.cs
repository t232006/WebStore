using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Base
{
    public class Blog :NamedEntity
    {
        public string Author { get; set; } = null!;
        public string? PictureUrl { get; set; }
        public string TextBlock { get; set; } = null!;
        public DateTime PublicDate { get; set; }
        public short Rate { get; set; }
        public int BrandID { get; set; }
        public int SectionID { get; set; }
        ICollection<Comment> Comments = new HashSet<Comment>();
    }
}
