using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    [Index(nameof(Name), IsUnique=false)]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int? ParentID { get; set; }
        [ForeignKey(nameof(ParentID))]
        public Section? Parent { get; set; }
        public ICollection<Product> Products { get; set; } 
    }


}
