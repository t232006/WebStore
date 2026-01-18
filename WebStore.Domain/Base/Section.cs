using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int? ParentID { get; set; }
    }


}
