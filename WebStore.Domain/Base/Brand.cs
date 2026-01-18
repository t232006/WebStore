using WebStore.Domain.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Domain.Base
{
    [Index(nameof(Name), IsUnique =true)]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }


}
