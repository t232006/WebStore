using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Base.Interfaces;
using WebStore.Domain.Identity;

namespace WebStore.Domain.Base
{
    public class Comment : IEntity
    {
        public Comment()
        {
            PublicDate = DateTime.Now;
        }
        public int ID { get; set; }
        public required string Text { get; set; } = null!;
        public string AuthorID { get; set; }
        [ForeignKey(nameof(AuthorID))]
        public required User Author { get; set; }
        public int BlogID { get; set; }
        [ForeignKey(nameof(BlogID))]
        public Blog? Blog { get; set; }
        public DateTime PublicDate { get; set; }
        public int? CommentID { get; set; }
        [ForeignKey(nameof(CommentID))]
        public Comment? ParentComment { get; set; }
        ICollection<Comment>? ChildComments { get; set; } = new HashSet<Comment>();

    }
}
