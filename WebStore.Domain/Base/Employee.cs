using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    [Table("Employees")]
    [Index(nameof(Name))]
    public class Employee : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public string? Position { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public int Age { get => Convert.ToInt32((DateTime.Today - DateOfBirth).TotalDays / 365); }
        ICollection<Blog> Blogs { get; set; }
    }
}
