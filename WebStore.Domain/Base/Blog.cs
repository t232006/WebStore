 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Identity;

namespace WebStore.Domain.Base
{
    public class Blog :NamedEntity
    {
        public Blog()
        {
            PublicDate = DateTime.Now;
        }
        public string AuthorID { get; set; } = null!;
        [ForeignKey(nameof(AuthorID))]
        public User Author { get; set; }
        public string? PictureUrl { get; set; }
        public string TextBlock { get; set; } = null!;
        public DateTime PublicDate { get; set; }
        public short Rate { get; set; }
        public int? BrandID { get; set; }
        [ForeignKey(nameof(BrandID))]
        public Brand? Brand { get; set; }
        [ForeignKey(nameof(SectionID))]
        public Section? Section {get; set;}
        public int? SectionID { get; set; }
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
