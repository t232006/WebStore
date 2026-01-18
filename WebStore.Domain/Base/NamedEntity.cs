
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; } = null!;
    }

}
