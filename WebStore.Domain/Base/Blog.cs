using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebStore.Domain.Base
{
    public class Blog :NamedEntity
    {
        [ForeignKey(nameof(Author))]
        public int AuthorID { get; set; }
        public Visitor Author { get; set; }
        public string? PictureUrl { get; set; }
        public string TextBlock { get; set; } = null!;
        public DateTime PublicDate { get; set; }
        public short Rate { get; set; }
        [ForeignKey(nameof(Brand))]
        public int? BrandID { get; set; }
        public Brand? Brand { get; set; }
        public Section? Section {get;set;}
        [ForeignKey (nameof(Section))]
        public int? SectionID { get; set; }
        ICollection<Comment> Comments = new HashSet<Comment>();
    }
}
