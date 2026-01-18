
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; } = null!;
    }

}
