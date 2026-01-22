using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public class Comment : IEntity
    {
        public int ID { get; set; }
        public string Text { get; set; } = null!;
        
        public int AuthorID { get; set; }
        [ForeignKey(nameof(AuthorID))]
        public Visitor Author { get; set; }
        public int BlogID { get; set; }
        [ForeignKey(nameof(BlogID))]
        public Blog Blog { get; set; }

    }
}
