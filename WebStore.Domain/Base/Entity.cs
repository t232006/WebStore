
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public bool Equals(Entity? other)
        {
            if (ReferenceEquals(other, this)) return true;
            if (other is null) return false;
            return other.ID == ID;
        }
        public override bool Equals(object? other)
        {
            if (other is null) return false;
            if (other.GetType != this.GetType) return false;
            if (ReferenceEquals(other, this)) return true;
            return Equals((Entity)other);
        }
        public override int GetHashCode()
        {
            return ID;
        }
        public static bool operator ==(Entity left, Entity right) => Equals(left, right);
        public static bool operator !=(Entity left, Entity right) => !Equals(left, right);
    }

}
