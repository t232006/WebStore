using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Identity;

namespace WebStore.Domain.Base
{
    [Index(nameof(BlogID), IsUnique = false)]
    [PrimaryKey(nameof(BlogID),nameof(UserID))]
    public class BlogUserRate
    {   
        public int BlogID { get; set; }
        [ForeignKey(nameof(BlogID))]
        public Blog Blog { get; set; }
        public string UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
        [Column(TypeName ="decimal(5,2)")]
        public decimal Rate { get; set; }
    }
}
