
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Base.Interfaces;

namespace WebStore.Domain.Base
{
    [Index(nameof(Name))]
    public class Product : NamedEntity, IOrderedEntity
    {
        [Column(TypeName ="decimal(18,2)")]
        public Decimal Price { get; set; }
        [Required]
        public String ImageUrl { get; set; } = null!;
        public int SectionId { get; set; }
        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int Order { get ; set ; }
    }
}
