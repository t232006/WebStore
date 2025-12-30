using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        int? ParentID { get; set; }
    }


}
