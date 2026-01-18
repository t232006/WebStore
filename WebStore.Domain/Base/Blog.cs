using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Base
{
    public class Blog :NamedEntity
    {
        public string Author { get; set; } = null!;
        public string? PictureUrl { get; set; }
        public string Block { get; set; } = null!;
        public DateTime publicDate { get; set; }
        public short Rate { get; set; }
        public int BrandID { get; set; }
        public int SectionID { get; set; }
    }
}
