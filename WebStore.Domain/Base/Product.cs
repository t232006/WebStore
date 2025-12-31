
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public class Product : NamedEntity, IOrderedEntity
    {
        Decimal Price { get; set; }
        String ImageUrl { get; set; } = null!;
        int SectionID { get; set; }
        int BrandID { get; set; }
        public int Order { get ; set ; }
    }
}
