
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public Decimal Price { get; set; }
        public String ImageUrl { get; set; } = null!;
        public int SectionId { get; set; }
        public int BrandId { get; set; }
        public int Order { get ; set ; }
    }
}
