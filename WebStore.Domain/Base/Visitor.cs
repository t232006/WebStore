using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public class Visitor: NamedEntity, IOrderedEntity
    {
        [Required]
        public string login { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        [Required]
        public string e_mail { get; set; } = null!;
        public DateTime regData { get; set; }
        public int Order { get; set; }
        ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
    }
}
